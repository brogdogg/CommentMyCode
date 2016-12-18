﻿/******************************************************************************
 * File...: CSharpFileCommentProvider.cs
 * Remarks:
 */
using MB.VS.Extension.CommentMyCode.UserOptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode.Providers.csharp
{


  /************************** CSharpFileCommentProvider **********************/
  /// <summary>
  /// 
  /// </summary>
  [Export(typeof(ICommentProvider))] // Indicate we implement ICommentProvider
  [ExportMetadata("SupportedCommandTypes",
    (int)(SupportedCommandTypeFlag.File))] // Support file comments
  [ExportMetadata("SupportedExtension", ".cs")] // works with *.cs files
  public class CSharpFileCommentProvider : BaseCommentProvider
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
    protected override void InitializeProvider()
    {
    }

    protected override void Process()
    {
      var textDocument = (EnvDTE.TextDocument)Context.Document.Object("TextDocument");
      var spEditPoint = textDocument.StartPoint.CreateEditPoint();
      var headerFormat = Context.State.Options.FileHeaderTemplate;
      spEditPoint.Insert("/" + new String('*', Context.State.Options.MaxColumnWidth - 2) + "\n");
      if (headerFormat != null)
        foreach (var line in headerFormat.Split('\n'))
          spEditPoint.Insert(" * " + line + "\n");
      else
      {
        spEditPoint.Insert(" * File...: " + Context.Document.Name + "\n");
        spEditPoint.Insert(" * Remarks: \n");
      }
      spEditPoint.Insert(" */\n");
      spEditPoint.StartOfDocument();

      var epEditPoint = textDocument.EndPoint.CreateEditPoint();
      epEditPoint.Insert("/* End of document - " + Context.Document.Name + " */");
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
  } // end of class - CSharpFileCommentProvider


}


/* End CSharpFileCommentProvider.cs */
