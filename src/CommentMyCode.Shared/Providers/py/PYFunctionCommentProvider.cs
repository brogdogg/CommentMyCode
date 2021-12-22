/******************************************************************************
 * File...: PYFunctionCommentProvider.cs
 * Remarks:
 */
using MB.VS.Extension.CommentMyCode.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode.Providers.py
{


  /************************** PYFunctionCommentProvider **********************/
  /// <summary>
  /// 
  /// </summary>
  [Export(typeof(ICommentProvider))] // Indicate we implement ICommentProvider
  [ExportMetadata("SupportedCommandTypes",
    (int)(SupportedCommandTypeFlag.Function))] // Support file comments
  [ExportMetadata("SupportedExtensions", new string[] { ".py" })] // works with *.py files
  public class PYFunctionCommentProvider : PYCommentProvider
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
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- ProcessBodyComments ---------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected override void ProcessBodyComments()
    {
      var endPoint = Context.CodeElement.EndPoint;
      var ep = endPoint.CreateEditPoint();
      var prop = Context.State.DTE.Properties["TextEditor", "Python"];
      Int16 indentSz = (Int16)prop.Item("IndentSize").Value;
      var colPad = endPoint.LineCharOffset - 1 + indentSz;
      ep.EndOfLine();
      ep.InsertLine("");
      ep.PadToColumn(colPad);
      ep.InsertLine(OpenCommentStr);
      ep.PadToColumn(colPad);
      ep.InsertLine("");
      ep.PadToColumn(colPad);
      ep.InsertLine("");
      ep.PadToColumn(colPad);
      ep.InsertLine("Arguments:");
      ep.PadToColumn(colPad);
      ep.InsertLine("");
      ep.PadToColumn(colPad);
      ep.InsertLine(CloseCommentStr);
      return;
    } // end of function - ProcessBodyComments

    /*----------------------- ProcessFooterComments -------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected override void ProcessFooterComments()
    {
      base.ProcessFooterComments();
    } // end of function - ProcessFooterComments

    /*----------------------- ProcessHeaderComments -------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected override void ProcessHeaderComments()
    {
      var ep = Context.CodeElement.StartPoint.CreateEditPoint();
      int offset = ep.LineCharOffset;
      ep.LineUp();
      ep.InsertLine("");
#if DEBUG
#warning TODO: Right now the header pad character is static, should be configurable
#endif
      ep.Insert(FormatPaddingComment(Context.CodeElement.Name, '-', offset));
    } // end of function - ProcessHeaderComments

    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/
  } // end of class - PYFunctionCommentProvider


}

/* End PYFunctionCommentProvider.cs */