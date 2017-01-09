﻿/******************************************************************************
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
      return;
    } // end of function - InitializeProvider

    /*----------------------- Process ---------------------------------------*/
    /// <summary>
    /// Comments based on the current comment command type
    /// </summary>
    protected override void Process()
    {
      Debug.WriteLine("CSharpCommentProvider.Comment-->");
    } // end of function - Comment
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