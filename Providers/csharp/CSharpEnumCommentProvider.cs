/******************************************************************************
 * File...: CSharpEnumCommentProvider.cs
 * Remarks:
 */
using EnvDTE;
using MB.VS.Extension.CommentMyCode.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode.Providers.csharp
{


  /************************** CSharpEnumCommentProvider **********************/
  /// <summary>
  /// 
  /// </summary>
  [Export(typeof(ICommentProvider))] // Indicate we implement ICommentProvider
  [ExportMetadata("SupportedCommandTypes",
    (int)(SupportedCommandTypeFlag.Enum))] // And support property comments
  [ExportMetadata("SupportedExtensions", new string[] { ".cs" })] // works with *.cs files
  public class CSharpEnumCommentProvider : CSharpCommentProvider
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
    /*----------------------- ProcessFooterComments -------------------------*/
    /// <summary>
    /// Inserts the footer 
    /// </summary>
    protected override void ProcessFooterComments()
    {
      var ep = Context.CodeElement.EndPoint.CreateEditPoint();
      var el = ep.GetLines(ep.Line, ep.Line + 1).Trim();
      if (el.EndsWith("}"))
      {
        ep.MoveToLineAndOffset(ep.Line, ep.LineCharOffset);
        ep.DeleteWhitespace(vsWhitespaceOptions.vsWhitespaceOptionsHorizontal);
        ep.EndOfLine();
        ep.Insert(" /* End of Enum - " + Context.CodeElement.Name + " */");
      } // end of if - 
      return;
    } // end of function - ProcessFooterComments

    /*----------------------- ProcessHeaderComments -------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected override void ProcessHeaderComments()
    {
      // Create an edit point to work with
      var ep = Context.CodeElement.StartPoint.CreateEditPoint();
      int padVal = Context.CodeElement.StartPoint.LineCharOffset;
      string name = Context.CodeElement.Name;
      // Move up on line from the code element
      ep.LineUp();
      // Move to the end of the line
      ep.EndOfLine();
      // And then insert a new line to start generating the comment on
      ep.InsertLine("");

      // Finally insert the actual comments
      ep.InsertLine(FormatPaddingComment(name, '*', padVal));
      ep.PadToColumn(padVal);
      ep.InsertLine("/// <summary>");
      ep.PadToColumn(padVal);
      ep.InsertLine("/// ");
      ep.PadToColumn(padVal);
      ep.Insert("/// </summary>");
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
  } // end of class - CSharpEnumCommentProvider


}

/* End CSharpEnumCommentProvider.cs */
