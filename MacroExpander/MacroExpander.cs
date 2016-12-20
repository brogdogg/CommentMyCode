/******************************************************************************
 * File...: MacroExpander.cs
 * Remarks:
 */
using MB.VS.Extension.CommentMyCode.Context;
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
    IItemContext Context
    {
      get;
    }
    /************************ Methods ****************************************/
    /// <summary>
    /// Expands the string, expanding macros to their well-known values
    /// </summary>
    /// <param name="strToExpand"></param>
    /// <returns></returns>
    string Expand(string strToExpand);
    void Initialize(IItemContext context);
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
    /*----------------------- State -----------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    public IItemContext Context
    {
      get { return m_context; }
    } // end of property - State
    /************************ Construction ***********************************/
    /*----------------------- MacroExpander ---------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    public MacroExpander()
    {
      return;
    } // end of function - MacroExpander

    /*----------------------- MacroExpander ---------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    public MacroExpander(IItemContext context)
    {
      m_context = context;
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
    /// Expands any known macros specified in the string
    /// </summary>
    /// <param name="strToExpand">
    /// The string that could possibly contain macros
    /// </param>
    /// <returns>
    /// A different string from the original, with all known macros expanded
    /// </returns>
    public virtual string Expand(string strToExpand)
    {
      if (strToExpand != null)
      {
        strToExpand = strToExpand.Replace("{FILENAME}", Context.Document.Name);
        strToExpand = strToExpand.Replace("{YEAR}", DateTime.Now.Year.ToString());
        strToExpand = strToExpand.Replace("{DATE}", DateTime.Now.ToString());
      } // end of if - valid string
      return strToExpand;
    } // end of function - Expand

    /*----------------------- Initialize ------------------------------------*/
    /// <summary>
    /// Initializes this object with the specified state
    /// </summary>
    /// <param name="context"></param>
    public virtual void Initialize(IItemContext context)
    {
      m_context = context;
      return;
    } // end of function - Initialize
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
      if (disposing)
        m_context?.Dispose();
      m_context = null;
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
    private IItemContext m_context = null;
    /************************ Static *****************************************/
  } // end of class - MacroExpander


} // end of namespace - MB.VS.Extension.CommentMyCode.MacroExpander


/* End MacroExpander.cs */