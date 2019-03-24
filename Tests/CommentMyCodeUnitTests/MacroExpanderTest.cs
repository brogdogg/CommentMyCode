/******************************************************************************
 * File...: MacroExpanderTest.cs
 * Remarks:
 */
using MB.VS.Extension.CommentMyCode.Context;
using MB.VS.Extension.CommentMyCode.MacroExpander;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace CommentMyCodeUnitTests
{


  /************************** MacroExpanderTest ******************************/
  /// <summary>
  /// 
  /// </summary>
  [TestClass]
  public class MacroExpanderTest
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- CanDispose ------------------------------------*/
    /// <summary>
    /// Verifies the object disposes correctly
    /// </summary>
    [TestMethod]
    public void CanDispose()
    {
      var context = Substitute.For<IItemContext>();
      var me = new MacroExpander(context);
      Assert.AreEqual(context, me.Context);
      me.Dispose();
      Assert.AreEqual(null, me.Context);
      return;
    } // end of function - CanDispose

    /*----------------------- ConstructsWithContext -------------------------*/
    /// <summary>
    /// Verifies the macro expander can construct with a specified context
    /// </summary>
    [TestMethod]
    public void ConstructsWithContext()
    {
      var context = Substitute.For<IItemContext>();
      var me = new MacroExpander(context);
      Assert.AreEqual(context, me.Context);
      return;
    } // end of function - ConstructsWithContext

    /*----------------------- ConstructsWithDefaults ------------------------*/
    /// <summary>
    /// Verifies the macro expander can construct with just defaults, nothing
    /// being specified
    /// </summary>
    [TestMethod]
    public void ConstructsWithDefaults()
    {
      var me = new MacroExpander();
      Assert.AreEqual(me.Context, null);
      return;
    } // end of function - ConstructsWithDefaults

    /*----------------------- ExpandNullString ------------------------------*/
    /// <summary>
    /// Verifies the macro expander can expand a null string
    /// </summary>
    [TestMethod]
    public void ExpandNullString()
    {
      var me = new MacroExpander();
      Assert.AreEqual(null, me.Expand(null));
      return;
    } // end of function - ExpandNullString

    /*----------------------- ExpandValidString -----------------------------*/
    /// <summary>
    /// Verifies the macro expander can expand a valid string
    /// </summary>
    [TestMethod]
    public void ExpandValidString()
    {
      var context = Substitute.For<IItemContext>();
      var doc = Substitute.For<EnvDTE.Document>();
      var name = "TestDocument.cs";
      doc.Name.Returns(name);
      context.Document.Returns(doc);
      var me = new MacroExpander(context);
      string strToExpand = "{FILENAME}|{DATE}|{YEAR}";
      var expandedStrs = me.Expand(strToExpand).Split('|');
      Assert.AreEqual(3, expandedStrs.Length);
      Assert.AreEqual(expandedStrs[0], name);
      Assert.IsTrue(expandedStrs[1] != "{DATE}");
      Assert.AreEqual(expandedStrs[2], DateTime.Now.Year.ToString());
      return;
    } // end of function - ExpandValidString

    /*----------------------- InitializesAfterDefaults ----------------------*/
    /// <summary>
    /// Verifies the macro expander can correctly initialize after a default
    /// construction
    /// </summary>
    [TestMethod]
    public void InitializesAfterDefaults()
    {
      var context = Substitute.For<IItemContext>();
      var me = new MacroExpander();
      me.Initialize(context);
      Assert.AreEqual(context, me.Context);
      return;
    } // end of function - InitializesAfterDefaults

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
  } // end of class - MacroExpanderTest

}


/* End MacroExpanderTest.cs */
