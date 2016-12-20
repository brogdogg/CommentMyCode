/******************************************************************************
 * File...: MacroExpander.cs
 * Remarks:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode.MacroExpander
{


  /************************** IMacroExpander *********************************/
  /// <summary>
  /// Describes an object reponsible for expanding parsing strings and
  /// expanding macros referenced within the text
  /// </summary>
  public interface IMacroExpander : IDisposable
  {
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Methods ****************************************/
    /// <summary>
    /// Expands the string, expanding macros to their well-known values
    /// </summary>
    /// <param name="strToExpand"></param>
    /// <returns></returns>
    string Expand(string strToExpand);
  } // end of interface - IMacroExpander


  /************************** MacroExpander **********************************/
  /// <summary>
  /// Base implementation of <see cref="IMacroExpander"/>, intended to be used
  /// as is or inherited and extended
  /// </summary>
  public class MacroExpander : IMacroExpander
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /*----------------------- MacroExpander ---------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    public MacroExpander()
    {
      return;
    } // end of function - MacroExpander

    /************************ Methods ****************************************/
    /*----------------------- Dispose ---------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
      Dispose(true); GC.SuppressFinalize(this);
    } // end of function - Dispose

    /*----------------------- Expand ----------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strToExpand"></param>
    /// <returns></returns>
    public virtual string Expand(string strToExpand)
    {
      return strToExpand;
    } // end of function - Expand
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- Dispose ---------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
      return;
    } // end of function - Dispose
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/
  } // end of class - MacroExpander


} // end of namespace - MB.VS.Extension.CommentMyCode.MacroExpander


/* End MacroExpander.cs */