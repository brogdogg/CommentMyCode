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

namespace MB.VS.Extension.CommentMyCode.Providers
{


  /************************** BaseCommentProvider ****************************/
  /// <summary>
  /// 
  /// </summary>
  public abstract class BaseCommentProvider : BaseProvider, ICommentProvider
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- Comment ---------------------------------------*/
    /// <summary>
    /// Comments on the code at cursor position
    /// </summary>
    public void Comment()
    {
      EnvDTE.TextDocument td = (EnvDTE.TextDocument)Context.Document.Object("TextDocument");
      var sp = td.StartPoint.CreateEditPoint();
      var ep = td.EndPoint.CreateEditPoint();
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
    protected virtual void Cleanup() { return; }
    protected virtual void Prepare() { return; }
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
    /// 
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
