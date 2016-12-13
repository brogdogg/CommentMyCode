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