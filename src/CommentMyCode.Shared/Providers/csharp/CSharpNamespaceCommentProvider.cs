/******************************************************************************
 * File...: CSharpNamespaceCommentProvider.cs
 * Remarks: ICommentProvider for C# namespace items.
 */
using System.ComponentModel.Composition;

namespace MB.VS.Extension.CommentMyCode.Providers.csharp
{
  /************************** CSharpNamespaceCommentProvider *****************/
  /// <summary>
  /// Provides commenting for C# namespaces
  /// </summary>
  /// <remarks>
  /// The <see cref="CSharpCommentProvider.ProcessFooterComments"/> method
  /// should process the end of namespace as expected. At this point the
  /// class is only provided to support the namespace command type.
  /// </remarks>
  [Export(typeof(ICommentProvider))] // Indicate we implement ICommentProvider
  [ExportMetadata("SupportedCommandTypes",
    (int)(SupportedCommandTypeFlag.Namespace))]
  [ExportMetadata("SupportedExtensions", new string[] { ".cs" })]
  public class CSharpNamespaceCommentProvider : CSharpCommentProvider
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
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

  } /* End of Class - CSharpNamespaceCommentProvider */

} /* End of Namespace - MB.VS.Extension.CommentMyCode.Providers.csharp */
/* End of document - CSharpNamespaceCommentProvider.cs */