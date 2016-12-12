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
    /// <remarks>
    /// Currently this is a limited method, it only searches for the following
    /// well known types:
    ///
    ///   - <see cref="EnvDTE.vsCMElement.vsCMElementProperty"/>
    ///   - <see cref="EnvDTE.vsCMElement.vsCMElementFunction"/>
    ///   - <see cref="EnvDTE.vsCMElement.vsCMElementEnum"/>
    ///   - <see cref="EnvDTE.vsCMElement.vsCMElementStruct"/>
    ///   - <see cref="EnvDTE.vsCMElement.vsCMElementClass"/>
    ///   - <see cref="EnvDTE.vsCMElement.vsCMElementInterface"/>
    ///
    /// </remarks>
    public static EnvDTE.CodeElement GetCodeElement(this EnvDTE.TextPoint tp)
    {
      EnvDTE.CodeElement retval = null;
      retval = tp.CodeElement[EnvDTE.vsCMElement.vsCMElementProperty];
      if (retval == null)
        if (null == (retval = tp.CodeElement[EnvDTE.vsCMElement.vsCMElementFunction]))
          if (null == (retval = tp.CodeElement[EnvDTE.vsCMElement.vsCMElementEnum]))
            if (null == (retval = tp.CodeElement[EnvDTE.vsCMElement.vsCMElementStruct]))
              if (null == (retval = tp.CodeElement[EnvDTE.vsCMElement.vsCMElementClass]))
                retval = tp.CodeElement[EnvDTE.vsCMElement.vsCMElementInterface];

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
