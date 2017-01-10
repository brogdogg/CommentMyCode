/******************************************************************************
 * File...: CCommentProvider.cs
 * Remarks:
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode.Providers.c
{


  /************************** CCommentProvider *******************************/
  /// <summary>
  /// 
  /// </summary>
  [Export(typeof(ICommentProvider))] // Indicate we implement ICommentProvider
  [ExportMetadata("SupportedCommandTypes",
    (int)(SupportedCommandTypeFlag.Function))] // Support file comments
  [ExportMetadata("SupportedExtensions", new string[] { ".cpp", ".c", ".h", ".hpp" })] // works with *.cs files
  public class CCommentProvider : BaseCommentProvider
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /*----------------------- CCommentProvider ------------------------------*/
    /// <summary>
    /// Default constructor setting up open/close comment strings for the base
    /// </summary>
    public CCommentProvider() : base("/*", "*/")
    {
    } // end of function - CCommentProvider

    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- Process ---------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected override void Process()
    {
      Debug.WriteLine("CCommentProvider.Process-->");
      Debug.WriteLine("CCommentProvider.Process<--");
    } // end of function - Process
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/
  } // end of class - CCommentProvider


}


/* End CCommentProvider.cs */
