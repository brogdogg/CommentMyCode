/******************************************************************************
 * File...: StringNormalizerTest.cs
 * Remarks:
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.VS.Extension.CommentMyCode;
using System.Collections.Generic;

namespace CommentMyCodeUnitTests
{


  /************************** StringNormalizerTest ***************************/
  /// <summary>
  /// 
  /// </summary>
  [TestClass]
  public class StringNormalizerTest
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- ConstructsWithDefaults ------------------------*/
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public void ConstructsWithDefaults()
    {
      StringNormalizer sn = new StringNormalizer();
      Assert.AreEqual(80, sn.MaxColumnCount);
      return;
    } // end of function - ConstructsWithDefaults

    /*----------------------- ConstructsWithNonDefaults ---------------------*/
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public void ConstructsWithNonDefaults()
    {
      int mxCnt = 1024;
      StringNormalizer sn = new StringNormalizer(mxCnt);
      Assert.AreEqual(mxCnt, sn.MaxColumnCount);
      return;
    } // end of function - ConstructsWithNonDefaults

    /*----------------------- NormalizeHandlesNegativeOffset ----------------*/
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void NormalizeHandlesNegativeOffset()
    {
      StringNormalizer sn = new StringNormalizer();
      foreach (var str in sn.Normalize("", -1)) ;
      return;
    } // end of function - NormalizeHandlesNegativeOffset


    /*----------------------- NormalizeHandlesNull --------------------------*/
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void NormalizeHandlesNull()
    {
      StringNormalizer sn = new StringNormalizer();
      foreach (var str in sn.Normalize(null)) ;
      return;
    } // end of function - NormalizeHandlesNull


    /*----------------------- NormalizeHandlesOutOfRange --------------------*/
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void NormalizeHandlesOutOfRange()
    {
      StringNormalizer sn = new StringNormalizer();
      foreach (var str in sn.Normalize("", sn.MaxColumnCount + 30)) ;
      return;
    } // end of function - NormalizeHandlesOutOfRange

    /*----------------------- NormalizeLongContinuousString -----------------*/
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public void NormalizeLongContinuousString()
    {
      StringNormalizer sn = new StringNormalizer(4);
      List<string> strs = new List<string>();
      var tmp = "testtesttest";
      foreach (var str in sn.Normalize(tmp, 0))
        strs.Add(str);

      Assert.AreEqual(1, strs.Count);
      Assert.AreEqual(tmp, strs[0]);
      return;
    } // end of function - NormalizeLongContinuousString

    /*----------------------- NormailizeLongString --------------------------*/
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public void NormailizeLongString()
    {
      StringNormalizer sn = new StringNormalizer(4);
      List<string> strs = new List<string>();
      var tmp = "test test test";
      foreach (var str in sn.Normalize(tmp, 0))
        strs.Add(str);

      Assert.AreEqual(3, strs.Count);
      Assert.AreEqual("test", strs[0]);
      return;
    } // end of function - NormailizeLongString

    /*----------------------- NormalizeShortLongContinuousString ------------*/
    [TestMethod]
    public void NormalizeShortLongContinuousString()
    {
      StringNormalizer sn = new StringNormalizer(4);
      List<string> strs = new List<string>();
      var tmp = "test testtesttest test";
      foreach (var str in sn.Normalize(tmp, 0))
        strs.Add(str);

      Assert.AreEqual(3, strs.Count);
      Assert.AreEqual("test", strs[0]);
      return;
    } // end of function - NormalizeShortLongContinuousString


    /*----------------------- NormalizeShortString --------------------------*/
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public void NormalizeShortString()
    {
      StringNormalizer sn = new StringNormalizer();
      List<string> strs = new List<string>();
      foreach (var str in sn.Normalize("test", 0))
        strs.Add(str);

      Assert.AreEqual(1, strs.Count);
      Assert.AreEqual("test", strs[0]);
      return;
    } // end of function - NormalizeShortString




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
  } // end of class - StringNormalizerTest


}


/* End StringNormalizerTest.cs */