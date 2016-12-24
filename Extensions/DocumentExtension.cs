/******************************************************************************
 * File...: DocumentExtension.cs
 * Remarks:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode.Extensions
{


  /************************** DocumentExtension ******************************/
  /// <summary>
  /// Provides various extension methods for the <see cref="EnvDTE.Document"/>
  /// object
  /// </summary>
  public static class DocumentExtension
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- GetActiveEditPoint ----------------------------*/
    /// <summary>
    /// Gets an edit point referencing the current active point in the document
    /// </summary>
    /// <param name="doc">
    /// A valid <see cref="EnvDTE.Document"/> object
    /// </param>
    /// <returns>
    /// An edit point pointing at the current active point in the document
    /// </returns>
    public static EnvDTE.EditPoint GetActiveEditPoint(this EnvDTE.Document doc)
    {
      var td = doc.Object("TextDocument") as EnvDTE.TextDocument;
      var ts = doc.GetTextSelection();
      return td.CreateEditPoint(ts.ActivePoint);
    } // end of function - GetActiveEditPoint

    /*----------------------- GetEndEditPoint -------------------------------*/
    /// <summary>
    /// Creates a new edit point at the end point of the document
    /// </summary>
    /// <param name="doc">
    /// Document to get the end point of
    /// </param>
    /// <returns>
    /// A newly created <see cref="EnvDTE.EditPoint"/> pointing at the end of
    /// the document
    /// </returns>
    public static EnvDTE.EditPoint GetEndEditPoint(this EnvDTE.Document doc)
    {
      var textDocument = (EnvDTE.TextDocument)doc.Object("TextDocument");
      return textDocument.EndPoint.CreateEditPoint();
    } // end of function - GetEndEditPoint

    /*----------------------- GetExtension ----------------------------------*/
    /// <summary>
    /// Gets the extension of the document
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    public static string GetExtension(this EnvDTE.Document doc)
    {
      return System.IO.Path.GetExtension(doc.FullName);
    } // end of function - GetExtension

    /*----------------------- GetStartEditPoint -----------------------------*/
    /// <summary>
    /// Creates a new edit point at the start point of the document
    /// </summary>
    /// <param name="doc">
    /// Document to get the end point of
    /// </param>
    /// <returns>
    /// A newly created <see cref="EnvDTE.EditPoint"/> pointing at the start
    /// of the document
    /// </returns>
    public static EnvDTE.EditPoint GetStartEditPoint(this EnvDTE.Document doc)
    {
      var textDocument = (EnvDTE.TextDocument)doc.Object("TextDocument");
      return textDocument.StartPoint.CreateEditPoint();
    } // end of function - GetStartEditPoint

    /*----------------------- GetTextSelection ------------------------------*/
    /// <summary>
    /// Gets the <see cref="EnvDTE.Document.Selection"/> as a 
    /// <see cref="EnvDTE.TextSelection"/> object
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    public static EnvDTE.TextSelection GetTextSelection(this EnvDTE.Document doc)
    {
      return (EnvDTE.TextSelection)doc.Selection;
    } // end of function - GetTextSelection

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
  } // end of class - DocumentExtension


}


/* End DocumentExtension.cs */