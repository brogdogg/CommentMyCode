/******************************************************************************
 * File...: ObjectExtension.cs
 * Remarks:
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommentMyCodeUnitTests.Extensions
{


  /************************** ObjectExtension **************************************/
  /// <summary>
  /// 
  /// </summary>
  public static class ObjectExtension
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- SetNonPublicProperty --------------------------*/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="propertyName"></param>
    /// <param name="value"></param>
    public static void SetNonPublicProperty(this object obj, string propertyName, object value)
    {
      var type = obj.GetType();
      var info = type.GetProperty(propertyName, System.Reflection.BindingFlags.SetProperty);
      if (info != null)
        info.SetValue(obj, value);
      return;
    } // end of function - SetNonPublicProperty

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
  } // end of class - ObjectExtension

}


/* End ObjectExtension.cs */