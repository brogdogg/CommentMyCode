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
    public override vsCMElement CodeElementScope
    {
      get
      {
        return EnvDTE.vsCMElement.vsCMElementFunction;
      }
    }
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
      return;
    }

    protected override void Process()
    {
      Debug.WriteLine("Function: " + this.CodeElement.FullName);
      Debug.WriteLine("Start Line: " + this.CodeElement.StartPoint.Line);
      Debug.WriteLine("End line:   " + this.CodeElement.EndPoint.Line);
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