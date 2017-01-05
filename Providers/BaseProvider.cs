/******************************************************************************
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
using System.Windows;

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

    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- Comment ---------------------------------------*/
    /// <summary>
    /// Comments on the code at cursor position
    /// </summary>
    public void Comment()
    {
      // Get a reference to the undo context
      var undoContext = Context.State.DTE.UndoContext;
      // Check to see if it was already opened
      bool wasInUndoContext = undoContext.IsOpen;
      // and we will need a flag indicating if we aborted the edit
      bool wasAborted = false;

      // If not already in an undo context, we will open our own
      if (!wasInUndoContext)
        undoContext.Open("CommentMyCode.VSIX", false);

      try
      {
        Prepare();
        Process();
        Cleanup();
      } // end of try - to actually perform the work needed
      catch (Exception exc)
      {
        // If we opened our own, then we will go ahead and abort the edit
        if (!wasInUndoContext)
        {
          undoContext.SetAborted();
          // And indicate we aborted the edit
          wasAborted = true;
        }
        Debug.WriteLine("Exception while commenting: " + exc.ToString());
        MessageBox.Show(
          "Error while attempting to provide comments.\n" + exc.ToString(),
          "Failed to Provide Comment(s)");
      } // end of catch - Exception
      finally
      {
        // Finally, if we did not abort the undo context then we need to close
        // it if we opened it initially
        if (!wasInUndoContext && !wasAborted) undoContext.Close();
      } // end of finally
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
      ActiveEditPoint = Context.Document.GetActiveEditPoint();
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
      base.InitializeProvider();
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