/******************************************************************************
 * File...: ItemContextTest.cs
 * Remarks:
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.VS.Extension.CommentMyCode.Context;
using MB.VS.Extension.CommentMyCode;
using NSubstitute;
using Microsoft.VisualStudio.Shell;
using EnvDTE;

namespace CommentMyCodeUnitTests
{


  /************************** ItemContextTest ********************************/
  /// <summary>
  /// 
  /// </summary>
  [TestClass]
  public class ItemContextTest
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- ConstructsWithDefaults ------------------------*/
    /// <summary>
    /// Verifies the ItemContext object constructs just fine with the defaults
    /// </summary>
    [TestMethod]
    public void ConstructsWithDefaults()
    {
      var ic = new ItemContext();
      Assert.AreEqual(ic.CommentType, SupportedCommandTypeFlag.Unknown);
      Assert.AreEqual(ic.Document, null);
      Assert.AreEqual(ic.Extension, null);
      Assert.AreEqual(ic.State, null);
      return;
    } // end of function - ConstructsWithDefaults

    /*----------------------- ConstructsWithState ---------------------------*/
    /// <summary>
    /// Currently it is difficult to pretend to the the VS environment, will
    /// come back to this
    /// </summary>
    [TestMethod]
    public void ConstructsWithState()
    {
      var state = Substitute.For<ICommentMyCodeState>();
      var dte = Substitute.For<EnvDTE80.DTE2>();
      var doc = Substitute.For<EnvDTE.Document>();
      var ts = Substitute.For<TextSelection>();
      //var tp = Substitute.For<TextPoint>();
      var vp = Substitute.For<VirtualPoint>();
      var ce = Substitute.For<EnvDTE.CodeElement>();
      ce.Kind.Returns(vsCMElement.vsCMElementProperty);
      //tp.CodeElement[EnvDTE.vsCMElement.vsCMElementProperty].Returns(ce);
      //ts.ActivePoint.Returns(tp);
      vp.CodeElement[EnvDTE.vsCMElement.vsCMElementProperty].Returns(ce);
      ts.ActivePoint.Returns(vp);
      var docPath = @"\temp\test.path";
      doc.Path.Returns(docPath);
      doc.FullName.Returns(docPath);
      doc.Selection.Returns(ts);
      dte.ActiveDocument.Returns(doc);
      state.DTE.Returns(dte);
      var ic = new ItemContext(state);
      Assert.AreEqual(ic.CommentType, SupportedCommandTypeFlag.Property);
      Assert.AreEqual(ic.Document, doc);
      return;
    } // end of function - ConstructsWithState


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
  } // end of class - ItemContextTest


}

/* End ItemContextTest.cs */