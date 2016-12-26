﻿/******************************************************************************
 * File...: BaseProvider.cs
 * Remarks:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.VS.Extension.CommentMyCode.Context;
using System.Diagnostics;
using MB.VS.Extension.CommentMyCode.Extensions;
using EnvDTE;

namespace MB.VS.Extension.CommentMyCode.Providers
{


  /************************** BaseCommentProvider ****************************/
  /// <summary>
  /// Base abstract implementation of the <see cref="ICommentProvider"/>
  /// interface
  /// </summary>
  public abstract class BaseCommentProvider : BaseProvider, ICommentProvider
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /*----------------------- ActiveEditPoint -------------------------------*/
    /// <summary>
    /// Gets the active edit point
    /// </summary>
    public EnvDTE.EditPoint ActiveEditPoint
    {
      get;
      protected set;
    } // end of property - ActiveEditPoint

    /*----------------------- CodeElement -----------------------------------*/
    /// <summary>
    /// Gets the code element associated with the active edit point
    /// </summary>
    public EnvDTE.CodeElement CodeElement
    {
      get;
      protected set;
    } // end of property - CodeElement

    /*----------------------- CodeElementScope ------------------------------*/
    /// <summary>
    /// Gets the scope of the code element to get, expected that extenders of
    /// this class to provide the type of element they expect
    /// </summary>
#if DEBUG
#warning We already look at the code element to figure out the kind for figuring out the provider, we could just use that
#endif
    public abstract EnvDTE.vsCMElement CodeElementScope
    {
      get;
    } // end of property - CodeElementScope

    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- Comment ---------------------------------------*/
    /// <summary>
    /// Comments on the code at cursor position
    /// </summary>
    public void Comment()
    {
#if DEBUG
#warning Currently forcing a save when it is a newly created/added project item, because things are null until saved the first time
#endif
      if (!Context.Document.Saved)
        Context.Document.Save();

      // Create a backup, in case there is an exception thrown
      var sp = Context.Document.GetStartEditPoint();
      var ep = Context.Document.GetEndEditPoint();
      sp.Copy(ep);
      try
      {
        Prepare();
        Process();
        Cleanup();
      }
      catch (Exception exc)
      {
        ep.EndOfDocument();
        sp.Delete(ep);
        sp.Paste();
        Debug.WriteLine("Exception while commenting: " + exc.ToString());
      }
      return;
    } // end of function - Comment

    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /// <summary>
    /// Cleans up after processing
    /// </summary>
    protected virtual void Cleanup() { return; }

    /*----------------------- InitializeActiveEditPoint ---------------------*/
    /// <summary>
    /// 
    /// </summary>
    public virtual void InitializeActiveEditPoint()
    {
      ActiveEditPoint = Context.Document.GetActiveEditPoint();
    } // end of function - InitializeActiveEditPoint

    /*----------------------- InitializeCodeElement -------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected virtual void InitializeCodeElement()
    {
      this.CodeElement = Context.Document.GetCodeElementAtActiveEditPoint(CodeElementScope);
      return;
    } // end of function - InitializeCodeElement

    /*----------------------- InitializeProvider ----------------------------*/
    /// <summary>
    /// Overridden to initialize various aspects of the provider that may need
    /// to be customized based on the provider
    /// </summary>
    /// <remarks>
    /// Initializes the code element property as well as the active edit point
    /// property
    /// </remarks>
    protected override void InitializeProvider()
    {
      InitializeActiveEditPoint();
      InitializeCodeElement();
      return;
    } // end of function - InitializeProvider

    /// <summary>
    /// Insert the data into the edit point
    /// </summary>
    /// <param name="ep"></param>
    /// <param name="msg"></param>
    protected virtual void InsertIntoDoc(EditPoint ep, string msg)
    {
      ep.Insert(msg);
      return;
    }

    /// <summary>
    /// Inserts a line into the document pointed to by the edit point
    /// </summary>
    /// <param name="ep">
    /// Edit point to insert at
    /// </param>
    /// <param name="msg">
    /// Message to write to edit point as a new line
    /// </param>
    protected virtual void InsertLineIntoDoc(EditPoint dp, string msg)
    {
      InsertIntoDoc(dp, msg + Environment.NewLine);
    }

    /// <summary>
    /// Prepare for processing
    /// </summary>
    protected virtual void Prepare() { return; }

    /// <summary>
    /// Process
    /// </summary>
    protected abstract void Process();
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    string[] m_originalLines = null;
    /************************ Static *****************************************/
  } // end of class - BaseCommentProvider


  /************************** BaseFileCommentProvider ************************/
  /// <summary>
  /// 
  /// </summary>
  public abstract class BaseFileCommentProvider : BaseCommentProvider
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /*----------------------- CodeElementScope ------------------------------*/
    /// <summary>
    /// Defaults to <see cref="vsCMElement.vsCMElementOther"/> scope, since we
    /// are dealing with just a file, not really a code element
    /// </summary>
    public override vsCMElement CodeElementScope
    {
      get { return vsCMElement.vsCMElementOther; }
    } // end of property - CodeElementScope
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /*----------------------- Normalizer ------------------------------------*/
    /// <summary>
    /// Gets the normalizer to use for normalizing strings for a max column
    /// width
    /// </summary>
    protected StringNormalizer Normalizer
    {
      get { return m_normalizer; }
    } // end of property - Normalizer

    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- InitializeProvider ----------------------------*/
    /// <summary>
    /// Initializes the base file comment provider to create the string
    /// normalizer
    /// </summary>
    protected override void InitializeProvider()
    {
      InitializeActiveEditPoint();
      m_normalizer = new StringNormalizer(Context.State.Options.MaxColumnWidth);
    } // end of function - InitializeProvider

    /*----------------------- Process ---------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected override void Process()
    {
      // Get the start point edit point
      var spEditPoint = Context.Document.GetStartEditPoint();
      // And get the header format from the options
      var headerFormat = Context.State.Options.FileHeaderTemplate;
      // Go ahead and process the comments for the header
      ProcessHead(spEditPoint, headerFormat);
      spEditPoint.StartOfDocument();

      // And then process the comments for the foot of the document
      ProcessFoot(Context.Document.GetEndEditPoint());

      return;
    } // end of function - Process

    /// <summary>
    /// Processes the document for the header comments
    /// </summary>
    /// <param name="startEditPoint">
    /// A edit point pointing at the current document
    /// </param>
    /// <param name="headerTemplate"></param>
    protected abstract void ProcessHead(EnvDTE.EditPoint startEditPoint, string headerTemplate);

    /// <summary>
    /// Processes the document for the footer comments
    /// </summary>
    /// <param name="endEditPoint">
    /// A edit point pointing at the end of the current document
    /// </param>
    protected abstract void ProcessFoot(EnvDTE.EditPoint endEditPoint);
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    StringNormalizer m_normalizer = null;
    /************************ Static *****************************************/
  } // end of class - BaseFileCommentProvider


  /************************** BaseProvider ***********************************/
  /// <summary>
  /// Provides an abstract implementation of <see cref="IProvider"/>
  /// </summary>
  public abstract class BaseProvider : IProvider
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /*----------------------- Context ---------------------------------------*/
    /// <summary>
    /// Gets the context object the provider was initialized with
    /// </summary>
    public IItemContext Context
    {
      get { return m_context; }
      protected set { m_context = value; }
    } // end of property - Context
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- Initialize ------------------------------------*/
    /// <summary>
    /// Initializes the provider with the context
    /// </summary>
    /// <param name="context">
    /// Context object to initialize with
    /// </param>
    public void Initialize(IItemContext context)
    {
      Context = context;
      InitializeProvider();
      return;
    } // end of function - Initialize
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /// <summary>
    /// Initializes the provider
    /// </summary>
    protected abstract void InitializeProvider();
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    IItemContext m_context = null;
    /************************ Static *****************************************/

  } // end of class - BaseProvider


}

/* End BaseProvider.cs */