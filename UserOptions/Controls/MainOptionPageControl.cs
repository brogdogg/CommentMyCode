/******************************************************************************
 * File...: MainOptionPageControl.cs
 * Remarks:
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MB.VS.Extension.CommentMyCode.UserOptions.Controls
{


  /************************** MainOptionPageControl **************************/
  /// <summary>
  /// 
  /// </summary>
  public partial class MainOptionPageControl : UserControl
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /*----------------------- MainOptionPageControl -------------------------*/
    /// <summary>
    /// 
    /// </summary>
    public MainOptionPageControl()
    {
      InitializeComponent();
    } // end of function - MainOptionPageControl
    /************************ Methods ****************************************/
    /*----------------------- Initialize ------------------------------------*/
    /// <summary>
    /// Initializes the controls with the values from the options object
    /// </summary>
    public void Initialize()
    {
      uxTemplateDataTextBox.Text = mainOptionPage.FileHeaderTemplate;
    } // end of function - Initialize

    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    internal MainOptionPage mainOptionPage;
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/
  } // end of class - MainOptionPageControl


}


/* End MainOptionPageControl.cs */
