/******************************************************************************
 * File...: CSharpClassCommentProvider.cs
 * Remarks:
 */
using EnvDTE;
using MB.VS.Extension.CommentMyCode.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode.Providers.csharp
{


  /************************** CSharpClassCommentProvider *********************/
  /// <summary>
  /// Provides the logic for providing comments for a C# class
  /// </summary>
  [Export(typeof(ICommentProvider))] // Indicate we implement ICommentProvider
  [ExportMetadata("SupportedCommandTypes",
    (int)(SupportedCommandTypeFlag.Class))] // Support class comments
  [ExportMetadata("SupportedExtensions", new string[] { ".cs" })] // works with *.cs files
  public class CSharpClassCommentProvider : CSharpCommentProvider
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /*----------------------- IsInterface -----------------------------------*/
    /// <summary>
    /// Gets/Sets a flag indicating if we are dealing with an interface or not
    /// </summary>
    protected bool IsInterface
    {
      get;
      set;
    } = false; // end of property - IsInterface

    /************************ Construction ***********************************/
    /*----------------------- InitializeProvider ----------------------------*/
    /// <summary>
    /// Initializes the <see cref="IsInterface"/> flag
    /// </summary>
    protected override void InitializeProvider()
    {
      base.InitializeProvider();
      IsInterface = (Context.CodeElement as CodeInterface) != null;
    } // end of function - InitializeProvider

    /*----------------------- ProcessBodyComments ---------------------------*/
    /// <summary>
    /// Inserts the body of the comment for the classes and interfaces
    /// </summary>
    protected override void ProcessBodyComments()
    {
      var accesses = new string[] { "public", "protected", "private" };
      var varTypes = new string[] { "Events", "Properties", "Construction", "Methods", "Fields", "Static" };
      var varIntTypes = new string[] { "Events", "Properties", "Methods" };
      var ep = Context.CodeElement.EndPoint.CreateEditPoint();
      var endPoint = Context.CodeElement.EndPoint;
      var prop = Context.State.DTE.Properties["TextEditor", "CSharp"];
      Int16 indentSz = (Int16)prop.Item("IndentSize").Value;
      var colPad = endPoint.LineCharOffset - 1 + indentSz;
      ep.LineUp();
      ep.EndOfLine();
      if (IsInterface)
      {
        foreach (var varType in varIntTypes)
        {
          ep.InsertLine("");
          ep.Insert(FormatPaddingComment(varType, '*', colPad));
        }
      }
      else
        foreach (var access in accesses)
        {
          ep.InsertLine("");
          ep.Insert(FormatPaddingComment(access.ToUpper(), '*', colPad));
          foreach (var varType in varTypes)
          {
            ep.InsertLine("");
            ep.Insert(FormatPaddingComment(varType, '*', colPad));
          }
          ep.InsertLine("");
        }
      return;
    } // end of function - ProcessBodyComments

    /*----------------------- ProcessFooterComments -------------------------*/
    /// <summary>
    /// Inserts the footer comment for the classes and interfaces
    /// </summary>
    protected override void ProcessFooterComments()
    {
      // Create the edit point to work with
      var ep = Context.CodeElement.EndPoint.CreateEditPoint();
      // et the line data and only do something if the class appears to not
      // have something already associated with it
      var el = ep.GetLines(ep.Line, ep.Line + 1).Trim();
      if (el.EndsWith("}"))
      {
        ep.MoveToLineAndOffset(ep.Line, ep.LineCharOffset);
        ep.DeleteWhitespace(vsWhitespaceOptions.vsWhitespaceOptionsHorizontal);
        ep.EndOfLine();
        if (IsInterface)
          ep.Insert(" /* End of interface - " + Context.CodeElement.Name + " */");
        else
          ep.Insert(" /* End of class - " + Context.CodeElement.Name + " */");
      } // end of if - ends with an expected character
      return;
    } // end of function - ProcessFooterComments

    /*----------------------- ProcessHeaderComments -------------------------*/
    /// <summary>
    /// Inserts the header comment of the class/interface
    /// </summary>
    protected override void ProcessHeaderComments()
    {
      // Create an edit point to work with
      var ep = Context.CodeElement.StartPoint.CreateEditPoint();
      int padVal = Context.CodeElement.StartPoint.LineCharOffset;
      string name = Context.CodeElement.Name;
      // Move up on line from the code element
      ep.LineUp();
      // Move to the end of the line
      ep.EndOfLine();
      // And then insert a new line to start generating the comment on
      ep.InsertLine("");

      // Finally insert the actual comments
      ep.InsertLine(FormatPaddingComment(name, '*', padVal));
      ep.PadToColumn(padVal);
      ep.InsertLine("/// <summary>");
      ep.PadToColumn(padVal);
      ep.InsertLine("/// ");
      ep.PadToColumn(padVal);
      ep.Insert("/// </summary>");
      return;
    } // end of function - ProcessHeaderComments

    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/
  } // end of class - CSharpClassCommentProvider


}

/* End CSharpClassCommentProvider.cs */
