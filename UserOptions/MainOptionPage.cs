/******************************************************************************
 * File...: MainOptionPage.cs
 * Remarks: Provides the default options for our main settings
 */
using MB.VS.Extension.CommentMyCode.UserOptions.Controls;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MB.VS.Extension.CommentMyCode.UserOptions
{


  /************************** MainOptionPage *********************************/
  /// <summary>
  /// Extends the VS <see cref="DialogPage"/> to provide the basic options
  /// for this module
  /// </summary>
  /// <remarks>
  /// Followed some example from
  /// https://msdn.microsoft.com/en-us/library/bb165657.aspx
  /// </remarks>
  //[Guid("D7BE6D81-4B5D-40EE-868F-00250A2D56CE")]
  public class MainOptionPage : DialogPage
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /*----------------------- FileHeaderTemplate ----------------------------*/
    /// <summary>
    /// Gets/Sets the file header template for comments
    /// </summary>
    [Category("Main")]
    [DisplayName("File Header")]
    [Description("File Header")]
    public string FileHeaderTemplate
    {
      get { return m_fileHeaderTemplate; }
      set { m_fileHeaderTemplate = value; }
    } // end of property - FileHeaderTemplate

    /*----------------------- MaxColumnWidth --------------------------------*/
    /// <summary>
    /// Gets/Sets the maximum column index, used to format commenting
    /// </summary>
    [Category("Main")]
    [DisplayName("Max Column")]
    [Description("The maximum column for commenting")]
    public int MaxColumnWidth
    {
      get { return m_maxColumnWidth; }
      set { m_maxColumnWidth = value; }
    } // end of property - MaxColumnWidth
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /*----------------------- Window ----------------------------------------*/
    /// <summary>
    /// Gets the custom control for these settings
    /// </summary>
    protected override IWin32Window Window
    {
      get
      {
        m_control = new MainOptionPageControl();
        m_control.mainOptionPage = this;
        m_control.Initialize();
        return m_control;
      } // end of get
    } // end of property - Window
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- OnApply ---------------------------------------*/
    /// <summary>
    /// Override the apply to grab the data from the control and update our
    /// instance
    /// </summary>
    /// <param name="e"></param>
    protected override void OnApply(PageApplyEventArgs e)
    {
      if(e.ApplyBehavior == ApplyKind.Apply)
      {
        MaxColumnWidth = (int)m_control.uxMaxColNumUpDown.Value;
        FileHeaderTemplate = m_control.uxTemplateDataTextBox.Text;
      }
      base.OnApply(e);
    } // end of function - OnApply
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    private string m_fileHeaderTemplate = null;
    private int m_maxColumnWidth = 80;
    MainOptionPageControl m_control = null;
    /************************ Static *****************************************/
  } // end of class - MainOptionPage


}


/* End MainOptionPage.cs */
