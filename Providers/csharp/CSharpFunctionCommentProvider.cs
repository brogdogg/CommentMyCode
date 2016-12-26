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

namespace MB.VS.Extension.CommentMyCode.Providers.csharp
{


  /************************** CSharpFunctionCommentProvider ******************/
  /// <summary>
  /// 
  /// </summary>
  [Export(typeof(ICommentProvider))] // Indicate we implement ICommentProvider
  [ExportMetadata("SupportedCommandTypes",
    (int)(SupportedCommandTypeFlag.Function))] // Support file comments
  [ExportMetadata("SupportedExtension", ".cs")] // works with *.cs files
  public class CSharpFunctionCommentProvider : BaseCommentProvider
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
    protected override void Process()
    {
      Debug.WriteLine("Function: " + Context.CodeElement.FullName);
      Debug.WriteLine("Start Line: " + Context.CodeElement.StartPoint.Line);
      Debug.WriteLine("End line:   " + Context.CodeElement.EndPoint.Line);
      var line = LineToInsert();
      Debug.WriteLine("Line to start Insert: " + line);
    }

    protected virtual int LineToInsert()
    {
      var td = Context.Document.Object("TextDocument") as EnvDTE.TextDocument;
      var ts = Context.Document.GetTextSelection();
      var fcm = Context.Document.ProjectItem.FileCodeModel;
      var ep = td.CreateEditPoint(ts.ActivePoint);
      ep.MoveToLineAndOffset(ep.Line - 1, ep.LineCharOffset);
      
      while(fcm.CodeElementFromPoint(ep, vsCMElement.vsCMElementAttribute) == null)
        ep.MoveToLineAndOffset(ep.Line - 1, ep.LineCharOffset);
      return ts.CurrentLine;
    }

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