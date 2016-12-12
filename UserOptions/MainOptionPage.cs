/******************************************************************************
 * File...: MainOptionPage.cs
 * Remarks:
 */
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode.UserOptions
{


  /************************** MainOptionPage *********************************/
  /// <summary>
  /// Extends the VS <see cref="DialogPage"/> to provide the basic options
  /// for this module
  /// </summary>
  public class MainOptionPage : DialogPage
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /*
    [Category("Main")]
    [DisplayName("File Header")]
    [Description("File Header")]
    public string FileHeaderTemplate
    {
      get { return m_fileHeaderTemplate; }
      set { m_fileHeaderTemplate = value; }
    }
    */
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
    private string m_fileHeaderTemplate = null;
    /************************ Static *****************************************/
  } // end of class - MainOptionPage


}


/* End MainOptionPage.cs */