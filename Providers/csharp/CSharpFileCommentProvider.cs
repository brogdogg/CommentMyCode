/******************************************************************************
 * File...: CSharpFileCommentProvider.cs
 * Remarks:
 */
using MB.VS.Extension.CommentMyCode.Extensions;
using MB.VS.Extension.CommentMyCode.UserOptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;

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
  public class CSharpFileCommentProvider : BaseFileCommentProvider
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /*----------------------- CSharpFileCommentProvider ---------------------*/
    /// <summary>
    /// 
    /// </summary>
    public CSharpFileCommentProvider() : base("/*", "*/")
    {
      return;
    } // end of function - CSharpFileCommentProvider

    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- ProcessFoot -----------------------------------*/
    /// <summary>
    /// Writes out the footer comments for a CSharp file
    /// </summary>
    /// <param name="endEditPoint">
    /// An edit point at the end of the document
    /// </param>
    protected override void ProcessFoot(EditPoint endEditPoint)
    {
      endEditPoint.Insert(
        "/* End of document - " + Context.Document.Name + " */");
    } // end of function - ProcessFoot

    /*----------------------- ProcessHead -----------------------------------*/
    /// <summary>
    /// Writes out the header comments for a CSharp file
    /// </summary>
    /// <param name="startEditPoint">
    /// The start edit point
    /// </param>
    /// <param name="headerTemplate">
    /// the header template information
    /// </param>
    protected override void ProcessHead(EditPoint startEditPoint, string headerTemplate)
    {
      var expander = Context.State.MacroExpander;
      // Insert the beginning of the header comments
      InsertLineIntoDoc(startEditPoint,
        "/" + new String('*', Context.State.Options.MaxColumnWidth - 2));

      // Process the header temlate, if there is one
      if (headerTemplate != null)
        foreach(var line in headerTemplate.Split('\n'))
          foreach (var normalizedStr in Normalizer.Normalize(expander.Expand(line), 3))
            InsertLineIntoDoc(startEditPoint, " * " + normalizedStr);
      // Or default to our own
      else
      {
        InsertLineIntoDoc(startEditPoint,
          expander.Expand(" * File...: {FILENAME}"));
        InsertLineIntoDoc(startEditPoint, " * Remarks: ");
      } // end of else

      // Close the comment section
      InsertLineIntoDoc(startEditPoint, " */");

      return;
    } // end of function - ProcessHead
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
