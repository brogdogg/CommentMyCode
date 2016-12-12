/******************************************************************************
 * File...: CommentMyCode.cs
 * Remarks:
 */
using EnvDTE;
using EnvDTE80;
using MB.VS.Extension.CommentMyCode.Context;
using MB.VS.Extension.CommentMyCode.Providers;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode
{


  /************************** CommentMyCode **********************************/
  /// <summary>
  /// Main command handler for comment my code
  /// </summary>
  public class CommentMyCode
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /*----------------------- DTE -------------------------------------------*/
    /// <summary>
    /// Gets the DTE2 service from the package
    /// </summary>
    public DTE2 DTE
    {
      get { return m_dte; }
    } // end of property - DTE
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
    /// Gets the static instance of <see cref="CommentMyCode"/>
    /// </summary>
    public static CommentMyCode Instance
    {
      get { return INSTANCE; }
    } // end of property - Instance
    /************************ Static *****************************************/
    /*----------------------- Initialize ------------------------------------*/
    /// <summary>
    /// Initializes expecting a valid package
    /// </summary>
    /// <param name="package"></param>
    /// <returns></returns>
    /// <remarks>
    /// Will throw an <see cref="InvalidOperationException"/> exception if
    /// this has already been initialized
    /// </remarks>
    public static CommentMyCode Initialize(Package package)
    {
      if (null == package)
        throw new ArgumentNullException("Package cannot be null");

      if (INSTANCE != null)
        throw new InvalidOperationException("Already initialized");

      ProviderFactory.Initialize();
      return (INSTANCE = new CommentMyCode(package));
    } // end of function - Initialize

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /*----------------------- Service ---------------------------------------*/
    /// <summary>
    /// Gets the package as a service provider
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
    /// Adds the comment commands to the menu command service
    /// </summary>
    protected virtual void AddCommentCommands(OleMenuCommandService service)
    {
      // Add in the file command
      var id = new CommandID(MenuGroup, CommentMyCodeCmdIDs.Comment.CommentFile);
      var command = new OleMenuCommand(new EventHandler(CommentFileHandler), id);
      service.AddCommand(command);

      // And add in the general command, which is responsible for parsing the
      // doc to figure out the comment sequence
      id = new CommandID(MenuGroup, CommentMyCodeCmdIDs.Comment.CommentGeneral);
      command = new OleMenuCommand(new EventHandler(CommentHandler), id);
      service.AddCommand(command);
      return;
    } // end of function - AddCommentCommands

    /*----------------------- CommentHandler --------------------------------*/
    /// <summary>
    /// Generic comment handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected virtual void CommentHandler(object sender, EventArgs args)
    {
      try
      {
        // Go ahead and build a context and execute the provider
        ExecuteCommentProvider(new ItemContext(this));
      }
      catch(Exception exc)
      {
        Debug.WriteLine("Exception: " + exc.ToString());
      }
      return;
    } // end of function - CommentHandler

    /*----------------------- CommentFileHandler ----------------------------*/
    /// <summary>
    /// Handler for the file comment command for specialized handling
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected virtual void CommentFileHandler(object sender, EventArgs args)
    {
      CommentHandler(sender, args);
    } // end of function - CommentFileHandler

    /*----------------------- ExecuteCommentProvider ------------------------*/
    /// <summary>
    /// Executes the comment provider with the specified context
    /// </summary>
    /// <param name="context">
    /// The main context object to exucute with
    /// </param>
    protected virtual void ExecuteCommentProvider(IItemContext context)
    {
      var provider = ProviderFactory.Instance.BuildProvider(context);
      provider?.Comment();
      return;
    } // end of function - ExecuteCommentProvider
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
      /// Represents a general comment command
      /// </summary>
      public const int CommentGeneral = 0x10241000;
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
      /// <summary>
      /// ID for commenting a struct
      /// </summary>
      public const int CommentStruct = 0x10241006;
      /// <summary>
      /// ID for commenting an interface
      /// </summary>
      public const int CommentInterface = 0x10241007;

    }

  } // end of class - CommentMyCodeCmdIDs


  /************************** SupportedCommandTypeFlag ***********************/
  /// <summary>
  /// Represents the different types of supported command types for commenting
  /// </summary>
  [Flags]
  public enum SupportedCommandTypeFlag
  {
    /// <summary>
    /// An unknown type
    /// </summary>
    Unknown = 0x0000,
    /// <summary>
    /// Class command type
    /// </summary>
    Class = 0x0001,
    /// <summary>
    /// Enum command type
    /// </summary>
    Enum = 0x0002,
    /// <summary>
    /// File command type
    /// </summary>
    File = 0x0004,
    /// <summary>
    /// Function command type
    /// </summary>
    Function = 0x0008,
    /// <summary>
    /// Interface type object
    /// </summary>
    Interface = 0x0010,
    /// <summary>
    /// Property command type
    /// </summary>
    Property = 0x0020,
    /// <summary>
    /// Struct object
    /// </summary>
    Struct =   0x0040

  } // end of enum - SupportedCommandTypeFlag


}


/* End CommentMyCode.cs */
