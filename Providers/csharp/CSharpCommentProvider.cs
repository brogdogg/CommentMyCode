/******************************************************************************
 * File...: CSharpCommentProvider.cs
 * Remarks:
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode.Providers.csharp
{


  /************************** CSharpCommentProvider **************************/
  /// <summary>
  /// Provides the logic needed to provide comment support for the C# language
  /// </summary>
  [Export(typeof(ICommentProvider))] // Indicate we implement ICommentProvider
  [ExportMetadata("SupportedCommandTypes",
    (int)(SupportedCommandTypeFlag.Class      // Support class comments
        | SupportedCommandTypeFlag.Enum       // Support enum comments
        | SupportedCommandTypeFlag.File       // Support file comments
        | SupportedCommandTypeFlag.Function   // Support function comments
        | SupportedCommandTypeFlag.Property))]// And support property comments
  [ExportMetadata("SupportedExtension", ".cs")] // works with *.cs files
  public class CSharpCommentProvider : BaseCommentProvider
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- Comment ---------------------------------------*/
    /// <summary>
    /// Comments based on the current comment command type
    /// </summary>
    public override void Comment()
    {
      Debug.WriteLine("CSharpCommentProvider.Comment-->");
    } // end of function - Comment
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- InitializeProvider ----------------------------*/
    /// <summary>
    /// Initializes this provider
    /// </summary>
    protected override void InitializeProvider()
    {
      Debug.WriteLine("CSharpCommentProvider.InitializeProvider-->");
      return;
    } // end of function - InitializeProvider
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/
  } // end of class - CSharpCommentProvider

}


/* End CSharpCommentProvider.cs */