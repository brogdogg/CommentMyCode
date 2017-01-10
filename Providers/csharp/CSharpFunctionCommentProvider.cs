/******************************************************************************
 * File...: CSharpFunctionCommentProvider.cs
 * Remarks:
 */
using MB.VS.Extension.CommentMyCode.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using MB.VS.Extension.CommentMyCode.Context;

namespace MB.VS.Extension.CommentMyCode.Providers.csharp
{

  /************************** EnvDetails *************************************/
  /// <summary>
  /// 
  /// </summary>
  public class EnvDetails
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    public int EndLine { get; set; } = 0;
    public int EndLineCharOffset { get; set; } = 0;
    public int EndLineLength { get; set; } = 0;
    public string Name { get; set; } = null;
    public int StartLine { get; set; } = 0;
    public int StartLineCharOffset { get; set; } = 0;
    /************************ Construction ***********************************/
    public EnvDetails(IItemContext context)
    {
      EndLine = context.CodeElement.EndPoint.Line;
      EndLineCharOffset = context.CodeElement.EndPoint.LineCharOffset;
      EndLineLength = context.CodeElement.EndPoint.LineLength;
      Name = context.CodeElement.Name;
      StartLine = context.CodeElement.StartPoint.Line;
      StartLineCharOffset = context.CodeElement.StartPoint.LineCharOffset;
    }
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/
  } // end of class - EnvDetails



  /************************** CSharpFunctionCommentProvider ******************/
  /// <summary>
  /// 
  /// </summary>
  [Export(typeof(ICommentProvider))] // Indicate we implement ICommentProvider
  [ExportMetadata("SupportedCommandTypes",
    (int)(SupportedCommandTypeFlag.Function))] // Support file comments
  [ExportMetadata("SupportedExtensions", new string[] { ".cs" })] // works with *.cs files
  public class CSharpFunctionCommentProvider : CSharpCommentProvider
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
    /*----------------------- Process ---------------------------------------*/
    /// <summary>
    /// Processes the main comment for a C# function
    /// </summary>
    /// <remarks>
    /// Currently needs to be refactored to a base class, but using for a
    /// sample and POC at the same time
    /// </remarks>
#if DEBUG
#warning TODO: Refactor the logic of providing header/footer comments for functions/methods
#endif
    protected override void Process()
    {
      var prop = Context.State.DTE.Properties["TextEditor", "CSharp"];
      var tabSz = prop.Item("TabSize").Value;
      Debug.WriteLine("TabSize: " + tabSz);
      var indentSz = prop.Item("IndentSize").Value;
      Debug.WriteLine("IndentSize: " + indentSz);

#if DEBUG
#warning TODO: Really should switch to have the line information update on the fly, so the order doesn't matter
#endif
      base.Process();
      return;
    } // end of function - Process

    /*----------------------- InsertParamComment ----------------------------*/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ep"></param>
    protected virtual void InsertParamComment(EditPoint ep, int offset)
    {
      var f = Context.CodeElement as CodeFunction;
      Debug.WriteLine("CodeFunction.DocComment (initially): " + f.DocComment);
      if (f != null)
      {
        ep.InsertLine("");
        ep.PadToColumn(offset);
        ep.InsertLine("/// <summary>");
        ep.PadToColumn(offset);
        ep.InsertLine("/// ");
        ep.PadToColumn(offset);
        ep.Insert("/// </summary>");
        foreach(var parm in f.Parameters)
        {
          CodeParameter codeParam = parm as CodeParameter;
          ep.InsertLine("");
          ep.PadToColumn(offset);
          ep.Insert("/// <param name=\"" + codeParam.Name + "\"></param>");
        }
        Debug.WriteLine("CodeFunction.DocComment: " + f.DocComment);
      }
      return;
    } // end of function - InsertParamComment

    /*----------------------- ProcessFooterComments -------------------------*/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="envDetails"></param>
    protected override void ProcessFooterComments()
    {
      var ep = Context.CodeElement.EndPoint.CreateEditPoint();
      var endLine = ep.GetLines(ep.Line, ep.Line + 1).Trim();
      if (endLine.EndsWith("}"))
      {
        ep.MoveToLineAndOffset(ep.Line, ep.LineCharOffset);
        ep.DeleteWhitespace(vsWhitespaceOptions.vsWhitespaceOptionsHorizontal);
        ep.EndOfLine();
        ep.Insert(" /* End of function - " + Context.CodeElement.Name + " */");
      }
      return;
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
#warning TODO: Assumed the pad character should stop at 30, should be configurable
#warning TODO: Assumed the end of the line would be 80, should be configurable
#warning TODO: Need to guard against really long function names that could go past the end of line, potentially leaving unescaped comment
#endif
      ep.Insert(FormatPaddingComment(Context.CodeElement.Name, '-', offset));
      InsertParamComment(ep, offset);
      return;
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
  } // end of class - CSharpFunctionCommentProvider


}

/* End CSharpFunctionCommentProvider.cs */
