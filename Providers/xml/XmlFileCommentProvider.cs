/******************************************************************************
 * File...: XmlFileCommentProvider.cs
 * Remarks:
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;

namespace MB.VS.Extension.CommentMyCode.Providers.xml
{


  /************************** XmlFileCommentProvider *************************/
  /// <summary>
  /// Provides a file comment provider for file comments
  /// </summary>
  [Export(typeof(ICommentProvider))] // Indicate we implement ICommentProvider
  [ExportMetadata("SupportedCommandTypes",
    (int)(SupportedCommandTypeFlag.File))] // Support file comments
  [ExportMetadata("SupportedExtensions", new string[] { ".xml" })] // works with *.cs files
  public class XmlFileCommentProvider : BaseFileCommentProvider
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /*----------------------- XmlFileCommentProvider ------------------------*/
    /// <summary>
    /// 
    /// </summary>
    public XmlFileCommentProvider()
      :base("<!--", "-->")
    {
      return;
    } // end of function - XmlFileCommentProvider

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
    /// Writes footer comments into the document
    /// </summary>
    /// <param name="endEditPoint"></param>
    protected override void ProcessFoot(EditPoint endEditPoint)
    {
      InsertLineIntoDoc(endEditPoint, "");
      endEditPoint.Insert(
        Context.State
               .MacroExpander
               .Expand("<!-- End of document - {FILENAME} -->"));
    } // end of function - ProcessFoot

    /*----------------------- ProcessHead -----------------------------------*/
    /// <summary>
    /// Writes header comments into the document
    /// </summary>
    /// <param name="startEditPoint"></param>
    /// <param name="headerTemplate"></param>
    protected override void ProcessHead(EditPoint startEditPoint, string headerTemplate)
    {
      var expander = Context.State.MacroExpander;
      InsertLineIntoDoc(startEditPoint, "<!--");
      if (headerTemplate != null)
        foreach(var line in headerTemplate.Split('\n'))
          foreach (var normalizedStr in Normalizer.Normalize(expander.Expand(line), 1))
            InsertLineIntoDoc(startEditPoint, " " + normalizedStr);
      else
      {
        InsertLineIntoDoc(startEditPoint,
          expander.Expand(" File...: {FILENAME}"));
        InsertLineIntoDoc(startEditPoint, " Remarks: ");
      }
      InsertLineIntoDoc(startEditPoint, "-->");
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
  } // end of class - XmlFileCommentProvider

}


/* End XmlFileCommentProvider.cs */