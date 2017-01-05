/******************************************************************************
 * File...: CSharpClassCommentProvider.cs
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


  /************************** CSharpClassCommentProvider *********************/
  /// <summary>
  /// 
  /// </summary>
  [Export(typeof(ICommentProvider))] // Indicate we implement ICommentProvider
  [ExportMetadata("SupportedCommandTypes",
    (int)(SupportedCommandTypeFlag.Class))] // Support class comments
  [ExportMetadata("SupportedExtension", ".cs")] // works with *.cs files
  public class CSharpClassCommentProvider : BaseCommentProvider
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
    /*----------------------- InsertBodyComment -----------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected virtual void InsertBodyComment()
    {
      var accesses = new string[] { "public", "protected", "private" };
      var varTypes = new string[] { "Events", "Properties", "Construction", "Methods", "Fields", "Static" };
      var ep = Context.CodeElement.EndPoint.CreateEditPoint();
      var endPoint = Context.CodeElement.EndPoint;
      var prop = Context.State.DTE.Properties["TextEditor", "CSharp"];
      Int16 indentSz = (Int16)prop.Item("IndentSize").Value;
      var colPad = endPoint.LineCharOffset - 1 + indentSz;
      ep.LineUp();
      ep.EndOfLine();
      foreach(var access in accesses)
      {
        ep.InsertLine("");
        ep.PadToColumn(colPad);
        ep.Insert("/*");
        ep.Insert(new string('=', 30 - ep.LineCharOffset));
        ep.Insert(" " + access.ToUpper() + " ");
        ep.Insert(new string('=', 78 - ep.LineCharOffset));
        ep.Insert("*/");
        foreach(var varType in varTypes)
        {
          ep.InsertLine("");
          ep.PadToColumn(colPad);
          ep.Insert("/*");
          ep.Insert(new string('*', 30 - ep.LineCharOffset));
          ep.Insert(" " + varType + " ");
          ep.Insert(new string('*', 78 - ep.LineCharOffset));
          ep.Insert("*/");
        }
        ep.InsertLine("");
      }
      return;
    } // end of function - InsertBodyComment

    /*----------------------- InsertFooterComment ---------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected virtual void InsertFooterComment()
    {
      var ep = Context.CodeElement.EndPoint.CreateEditPoint();
      var el = ep.GetLines(ep.Line, ep.Line + 1).Trim();
      if(el.EndsWith("}"))
      {
        ep.MoveToLineAndOffset(ep.Line, ep.LineCharOffset);
        ep.DeleteWhitespace(vsWhitespaceOptions.vsWhitespaceOptionsHorizontal);
        ep.EndOfLine();
        ep.Insert(" /* End of class - " + Context.CodeElement.Name + " */");
      } // end of if - ends with an expected character
      return;
    } // end of function - InsertFooterComment

    /*----------------------- InsertHeaderComment ---------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected virtual void InsertHeaderComment()
    {
      var ep = Context.CodeElement.StartPoint.CreateEditPoint();
      int padVal = Context.CodeElement.StartPoint.LineCharOffset;
      string name = Context.CodeElement.Name;
      ep.LineUp();
      ep.EndOfLine();
      ep.InsertLine("");
      ep.PadToColumn(padVal);
      ep.Insert("/*");
      ep.Insert(new string('*', 30 - ep.LineCharOffset));
      ep.Insert(" " + name + " ");
      ep.Insert(new string('*', 78 - ep.LineCharOffset));
      ep.Insert("*/");
      ep.InsertLine("");
      ep.PadToColumn(padVal);
      ep.InsertLine("/// <summary>");
      ep.PadToColumn(padVal);
      ep.InsertLine("/// ");
      ep.PadToColumn(padVal);
      ep.Insert("/// </summary>");
      return;
    } // end of function - InsertHeaderComment

    /*----------------------- Process ---------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected sealed override void Process()
    {
      InsertFooterComment();
      InsertBodyComment();
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
  } // end of class - CSharpClassCommentProvider


}

/* End CSharpClassCommentProvider.cs */