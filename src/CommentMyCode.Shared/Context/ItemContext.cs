/******************************************************************************
 * File...: ItemComment.cs
 * Remarks:
 */
using EnvDTE;
using EnvDTE80;
using MB.VS.Extension.CommentMyCode.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode.Context
{


  /************************** IItemContext ***********************************/
  /// <summary>
  /// Represents a context to process
  /// </summary>
  public interface IItemContext : IDisposable
  {
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /// <summary>
    /// Gets the code element associated with the context
    /// </summary>
    EnvDTE.CodeElement CodeElement { get; }
    /// <summary>
    /// The <see cref="SupportedCommandTypeFlag"/> flag indicating which state
    /// the document is in
    /// </summary>
    SupportedCommandTypeFlag CommentType { get; }
    /// <summary>
    /// Gets the current VS document
    /// </summary>
    EnvDTE.Document Document { get; }
    /// <summary>
    /// Gets the extension of the file
    /// </summary>
    string Extension { get; }
    /// <summary>
    /// Gets the state object
    /// </summary>
    ICommentMyCodeState State { get; }
    /************************ Methods ****************************************/
    /// <summary>
    /// Initializes with the specified state object
    /// </summary>
    /// <param name="state"></param>
    void Initialize(ICommentMyCodeState state);
    void Initialize(ICommentMyCodeState state, SupportedCommandTypeFlag commentType);
  } // end of interface - IItemContext


  /************************** ItemContext ************************************/
  /// <summary>
  /// Basic implementation of <see cref="IItemContext"/>
  /// </summary>
  public class ItemContext : IItemContext
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /*----------------------- CodeElement -----------------------------------*/
    /// <summary>
    /// Gets the code element associated with the context
    /// </summary>
    public EnvDTE.CodeElement CodeElement
    {
      get;
      protected set;
    } // end of property - CodeElement

    /*----------------------- CommentType -----------------------------------*/
    /// <summary>
    /// Gets the comment type based on the current cursor position
    /// </summary>
    public SupportedCommandTypeFlag CommentType
    {
      get;
      protected set;
    } = SupportedCommandTypeFlag.Unknown; // end of property - CommentType

    /*----------------------- Document --------------------------------------*/
    /// <summary>
    /// Gets the VS document object
    /// </summary>
    public EnvDTE.Document Document
    {
      get
      {
        return State?.DTE.ActiveDocument;
      }
    } // end of property - Document

    /*----------------------- Extension -------------------------------------*/
    /// <summary>
    /// Gets the extension of the current document, if any
    /// </summary>
    public string Extension
    {
      get
      {
        return Document?.GetExtension();
      }
    } // end of property - Extension

    /*----------------------- State -----------------------------------------*/
    /// <summary>
    /// Gets the state object
    /// </summary>
    public ICommentMyCodeState State
    {
      get;
      protected set;
    } = null; // end of property - State
    /************************ Construction ***********************************/
    /*----------------------- ItemContext -----------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    public ItemContext()
    {
      return;
    } // end of function - ItemContext

    /*----------------------- ItemContext -----------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    public ItemContext(ICommentMyCodeState state)
    {
      Initialize(state);
    } // end of function - ItemContext

    /*----------------------- ~ItemContext ----------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    ~ItemContext() { Dispose(false); }

    /************************ Methods ****************************************/
    /*----------------------- Dispose ---------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

    /*----------------------- Initialize ------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    public void Initialize(ICommentMyCodeState state)
    {
      Initialize(state, SupportedCommandTypeFlag.Unknown);
    } // end of function - Initialize

    /*----------------------- Initialize ------------------------------------*/
    /// <summary>
    /// Initializes the object based on the state object
    /// </summary>
    /// <param name="state"></param>
    public void Initialize(ICommentMyCodeState state, SupportedCommandTypeFlag commentType)
    {
      if (null == (State = state))
        throw new ArgumentNullException(nameof(state), "A valid state object must be provided");

      if ((CommentType = commentType) == SupportedCommandTypeFlag.Unknown)
        // Parse the comment type from the document
        ParseCommentType();
      return;
    } // end of function - Initialize
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- Dispose ---------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
      State = null;
      CommentType = SupportedCommandTypeFlag.Unknown;
      CodeElement = null;
      return;
    } // end of function - Dispose
    /*----------------------- ParseCommentType ------------------------------*/
    /// <summary>
    /// Parses the comment type from the current document
    /// </summary>
    protected virtual void ParseCommentType()
    {
      // Get the current selection
      if (Document?.Selection is TextSelection sel)
      {
        // From the text selection we should be able to get the active point
        // for inspection
        TextPoint tp = sel.ActivePoint;
        // Then check for well-known code elements
        var ce = this.CodeElement = tp.GetCodeElement();
        if (ce != null)
        {
          switch (ce.Kind)
          {
            case vsCMElement.vsCMElementClass:
              CommentType = SupportedCommandTypeFlag.Class;
              break;
            case vsCMElement.vsCMElementEnum:
              CommentType = SupportedCommandTypeFlag.Enum;
              break;
            case vsCMElement.vsCMElementFunction:
              CommentType = SupportedCommandTypeFlag.Function;
              break;
            case vsCMElement.vsCMElementInterface:
              CommentType = SupportedCommandTypeFlag.Interface;
              break;
            case vsCMElement.vsCMElementProperty:
              CommentType = SupportedCommandTypeFlag.Property;
              break;
            case vsCMElement.vsCMElementNamespace:
              CommentType = SupportedCommandTypeFlag.Namespace;
              break;
            default:
              CommentType = SupportedCommandTypeFlag.Unknown;
              break;
          } // end switch - code element kind
        } // end of if - code elemtn of current active point is valid
      } // end of if - valid insertion point
      return;
    } // end of function - ParseCommentType
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/
  } // end of class - ItemContext


}


/* End ItemComment.cs */