/******************************************************************************
 * File...: CSharpClassCommentProviderTest.cs
 * Remarks:
 */
using CommentMyCodeUnitTests.Extensions;
using MB.VS.Extension.CommentMyCode;
using MB.VS.Extension.CommentMyCode.Context;
using MB.VS.Extension.CommentMyCode.Providers.csharp;
using MB.VS.Extension.CommentMyCode.UserOptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace CommentMyCodeUnitTests.Providers.csharp
{


  /************************** CSharpClassCommentProviderTest *****************/
  /// <summary>
  /// 
  /// </summary>
  [TestClass]
  public class CSharpClassCommentProviderTest
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- Can_Initialize_With_Class ---------------------*/
    /// <summary>
    /// Verifies the <see cref="CSharpClassCommentProvider"/> class can
    /// initialize correctly with a class code element
    /// </summary>
    [TestMethod]
    public void Can_Initialize_With_Class()
    {
      var csharp = Substitute.ForPartsOf<CSharpClassCommentProvider>();
      var doc = Substitute.For<EnvDTE.Document>();
      var options = Substitute.ForPartsOf<MainOptionPage>();
      var state = Substitute.For<ICommentMyCodeState>();
      state.Options.Returns(options);

      EnvDTE.CodeElement codeInterface = Substitute.For<EnvDTE.CodeInterface>() as EnvDTE.CodeElement;

      var context = new ItemContext();
      context.SetNonPublicProperty("CodeElement", (EnvDTE.CodeElement)codeInterface);
      context.SetNonPublicProperty("Document", doc);
      context.SetNonPublicProperty("State", state);
      csharp.Initialize(context);
      var prop = typeof(CSharpClassCommentProvider).GetProperty("IsInterface", System.Reflection.BindingFlags.GetField);
      var isInterface = (bool)prop.GetValue(csharp);
      Assert.IsTrue(isInterface);
      return;
    } // end of function - Can_Initialize_With_Class

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
  } // end of class - CSharpClassCommentProviderTest

}


/* End CSharpClassCommentProviderTest.cs */
