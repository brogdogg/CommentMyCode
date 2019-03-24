/******************************************************************************
 * File...: TextPointExtension.cs
 * Remarks:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode.Extensions
{


  /************************** TextPointExtension *****************************/
  /// <summary>
  /// Provides various extension methods for the <see cref="EnvDTE.TextPoint"/>
  /// object
  /// </summary>
  public static class TextPointExtension
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/
    /*----------------------- GetCodeElement --------------------------------*/
    /// <summary>
    /// Gets the <see cref="EnvDTE.CodeElement"/> of the text point
    /// </summary>
    /// <param name="tp"></param>
    /// <returns></returns>
    public static EnvDTE.CodeElement GetCodeElement(this EnvDTE.TextPoint tp)
    {
      var scopes = Enum.GetValues(typeof(EnvDTE.vsCMElement));
      EnvDTE.CodeElement retval = null;
      foreach(var s in scopes)
      {
        try
        {
          var scope = (EnvDTE.vsCMElement)s;
          retval = tp.CodeElement[scope];
          if (retval != null) break;
        }
        catch {; }
      }
      return retval;
    } // end of function - GetCodeElement

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
  } // end of class - CodeElementExtension


}


/* End CodeElementExtension.cs */
