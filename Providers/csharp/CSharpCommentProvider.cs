/******************************************************************************
 * File...: CSharpCommentProvider.cs
 * Remarks:
 */
using EnvDTE;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode.Providers.csharp
{


  /************************** CSharpCommentProvider **************************/
  /// <summary>
  /// Provides the logic needed to provide comment support for the C# language
  /// </summary>
  public class CSharpCommentProvider : BaseCommentProvider
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /*----------------------- CSharpCommentProvider -------------------------*/
    /// <summary>
    /// Builds out the provider
    /// </summary>
    public CSharpCommentProvider() : base("/*", "*/")
    {
      return;
    } // end of function - CSharpCommentProvider

    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- InitializeProvider ----------------------------*/
    /// <summary>
    /// Initializes this provider
    /// </summary>
    protected override void InitializeProvider()
    {
      Debug.WriteLine("CSharpCommentProvider.InitializeProvider-->");
      base.InitializeProvider();
      return;
    } // end of function - InitializeProvider

    /*----------------------- ProcessFooterComments -------------------------*/
    /// <summary>
    /// Inserts the footer 
    /// </summary>
    protected override void ProcessFooterComments()
    {
      var ep = Context.CodeElement.EndPoint.CreateEditPoint();
      var el = ep.GetLines(ep.Line, ep.Line + 1).Trim();
      if (el.EndsWith("}"))
      {
        ep.MoveToLineAndOffset(ep.Line, ep.LineCharOffset);
        ep.DeleteWhitespace(vsWhitespaceOptions.vsWhitespaceOptionsHorizontal);
        ep.EndOfLine();
        ep.Insert(" /* End of " + Context.CommentType + " - " + Context.CodeElement.Name + " */");
      } // end of if - 
    } // end of function - ProcessFooterComments

    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/
  } // end of class - CSharpCommentProvider

}


/* End CSharpCommentProvider.cs */