/******************************************************************************
 * File...: CommentMyCode.cs
 * Remarks:
 */
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode
{

  /************************** CommentMyCodeCmdIDs ****************************/
  /// <summary>
  /// A collection of command IDs
  /// </summary>
  public static class CommentMyCodeCmdIDs
  {
    /*======================= PUBLIC ========================================*/
    /************************ Static *****************************************/
    /// <summary>
    /// Command IDs for commenting
    /// </summary>
    public static class Comment
    {
      /// <summary>
      /// ID for commenting a class
      /// </summary>
      public const int CommentClass = 0x10241001;
      /// <summary>
      /// ID for commenting an enum
      /// </summary>
      public const int CommentEnum = 0x10241002;
      /// <summary>
      /// ID for commenting a file
      /// </summary>
      public const int CommentFile = 0x10241003;
      /// <summary>
      /// ID for commenting a function
      /// </summary>
      public const int CommentFunction = 0x10241004;
      /// <summary>
      /// ID for commenting a property
      /// </summary>
      public const int CommentProperty = 0x10241005;

    }

  } // end of class - CommentMyCodeCmdIDs


  /************************** CommentMyCode **********************************/
  /// <summary>
  /// 
  /// </summary>
  public class CommentMyCode
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static Fields **********************************/
    /// <summary>
    ///  Represents the menu group for <see cref="CommentMyCode"/>
    /// </summary>
    public static readonly Guid MenuGroup = new Guid("DACDBE1C-C2E2-4069-8C6F-A6E0D9A330A1");
    /************************ Static Properties ******************************/
    /*----------------------- Instance --------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    public static CommentMyCode Instance
    {
      get { return INSTANCE; }
    } // end of property - Instance
    /************************ Static *****************************************/
    /*----------------------- Initialize ------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="package"></param>
    /// <returns></returns>
    public static CommentMyCode Initialize(Package package)
    {
      if (null == package)
        throw new ArgumentNullException("Package cannot be null");

      if (INSTANCE != null)
        throw new InvalidOperationException("Already initialized");

      return (INSTANCE = new CommentMyCode(package));
    } // end of function - Initialize

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /*----------------------- Service ---------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    public IServiceProvider Service
    {
      get { return m_package as IServiceProvider; }
    } // end of property - Service


    /************************ Construction ***********************************/
    /*----------------------- CommentMyCode ---------------------------------*/
    /// <summary>
    /// Constructs an instance with the specified <see cref="Package"/>
    /// </summary>
    /// <param name="package"></param>
    protected CommentMyCode(Package package)
    {
      m_package = package;
      IServiceProvider provider = package as IServiceProvider;
      m_dte = (DTE2)provider.GetService(typeof(DTE));

      var menu = provider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
      if (null != menu)
      {
        AddCommentCommands(menu);
      } // end of if - valid menu
      return;
    } // end of function - CommentMyCode
    /************************ Methods ****************************************/
    /*----------------------- AddCommentCommands ----------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected virtual void AddCommentCommands(OleMenuCommandService service)
    {
      var id = new CommandID(MenuGroup, CommentMyCodeCmdIDs.Comment.CommentClass);
      var command = new OleMenuCommand(new EventHandler(CommentClassHandler), id);
      service.AddCommand(command);

      id = new CommandID(MenuGroup, CommentMyCodeCmdIDs.Comment.CommentEnum);
      command = new OleMenuCommand(new EventHandler(CommentEnumHandler), id);
      service.AddCommand(command);
    } // end of function - AddCommentCommands

    /*----------------------- CommentClassHandler ---------------------------*/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected virtual void CommentClassHandler(object sender, EventArgs args)
    {
      WriteToOutputWindow("HEY HEY HEY HEY");
      return;
    } // end of function - CommentClassHandler

    /*----------------------- CommentEnumHandler ----------------------------*/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected virtual void CommentEnumHandler(object sender, EventArgs args)
    {
    } // end of function - CommentEnumHandler

    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- WriteToOutputWindow ---------------------------*/
    /// <summary>
    /// 
    /// </summary>
    private void WriteToOutputWindow(string str)
    {
      IVsOutputWindowPane windowPane = (IVsOutputWindowPane)Service.GetService(typeof(SVsGeneralOutputWindowPane));
      if(null != windowPane)
      {
        windowPane.OutputString(str);
      } // end of if - window pane is valid
      return;
    } // end of function - WriteToOutputWindow

    /************************ Fields *****************************************/
    private Package m_package = null;
    private DTE2 m_dte = null;
    /************************ Static *****************************************/
    private static CommentMyCode INSTANCE = null;
  } // end of class - CommentMyCode


}


/* End CommentMyCode.cs */
