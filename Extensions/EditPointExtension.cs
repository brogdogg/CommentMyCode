/******************************************************************************
 * File...: EditPointExtension.cs
 * Remarks:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode.Extensions
{


  /************************** EditPointExtension *****************************/
  /// <summary>
  /// Provides various extensions mthods for the <see cref="EnvDTE.EditPoint"/>
  /// class object
  /// </summary>
  public static class EditPointExtension
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- InsertLine ------------------------------------*/
    /// <summary>
    /// Inserts the data into the edit point appending with a new line
    /// </summary>
    /// <param name="ep">
    /// The edit point to insert data
    /// </param>
    /// <param name="data">
    /// The data to insert
    /// </param>
    public static void InsertLine(this EnvDTE.EditPoint ep, string data)
    {
      ep.Insert(data + Environment.NewLine);
      return;
    } // end of function - InsertLine
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
  } // end of class - EditPointExtension

}


/* End EditPointExtension.cs */
