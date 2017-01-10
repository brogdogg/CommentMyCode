/******************************************************************************
 * File...: CSharpPropertyCommentProvider
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


  /************************** CSharpPropertyCommentProvider ******************/
  /// <summary>
  /// Provides commenting for C# properties
  /// </summary>
  [Export(typeof(ICommentProvider))] // Indicate we implement ICommentProvider
  [ExportMetadata("SupportedCommandTypes",
    (int)(SupportedCommandTypeFlag.Property))]
  [ExportMetadata("SupportedExtensions", new string[] { ".cs" })] // works with *.cs files
  public class CSharpPropertyCommentProvider : CSharpCommentProvider
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
    /*----------------------- InsertFooterComment ---------------------------*/
    /// <summary>
    /// Inserts the footer 
    /// </summary>
    protected void InsertFooterComment()
    {
      var ep = Context.CodeElement.EndPoint.CreateEditPoint();
      var el = ep.GetLines(ep.Line, ep.Line + 1).Trim();
      if(el.EndsWith("}"))
      {
        ep.MoveToLineAndOffset(ep.Line, ep.LineCharOffset);
        ep.DeleteWhitespace(vsWhitespaceOptions.vsWhitespaceOptionsHorizontal);
        ep.EndOfLine();
        ep.Insert(" /* End of Property - " + Context.CodeElement.Name + " */");
      } // end of if - 
      return;
    } // end of function - InsertFooterComment

    /*----------------------- InsertHeaderComment ---------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected void InsertHeaderComment()
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
    } // end of function - InsertHeaderComment

    /*----------------------- Process ---------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected override void Process()
    {
      InsertFooterComment();
      InsertHeaderComment();
      return;
    } // end of function - Process
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/
  } // end of class - CSharpPropertyCommentProvider


}

/* End CSharpPropertyCommentProvider */