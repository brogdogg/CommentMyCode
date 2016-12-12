/******************************************************************************
 * File...: IProvider.cs
 * Remarks:
 */

using MB.VS.Extension.CommentMyCode.Context;
using System;

namespace MB.VS.Extension.CommentMyCode.Provider
{


  /************************** ICommentProvider *******************************/
  /// <summary>
  /// Extension to the <see cref="IProvider"/> class to provide an entry point
  /// for commenting on the current project item
  /// </summary>
  public interface ICommentProvider : IProvider
  {
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Methods ****************************************/
    /// <summary>
    /// Comments the current line, attempting to guess 
    /// </summary>
    void Comment();
  } // end of interface - ICommentProvider


  /************************** ICommentProviderData ***************************/
  /// <summary>
  /// The metadata to be used with the classes implementing
  /// <see cref="ICommentProvider"/> to be used with the Microsoft
  /// Extensibility Framework
  /// </summary>
  public interface ICommentProviderData
  {
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /// <summary>
    /// Supported file extension
    /// </summary>
    string SupportedExtension { get; }

    /// <summary>
    /// Flag indicating the types of <see cref="SupportedCommandTypeFlag"/>
    /// </summary>
    int SupportedCommandTypes { get; }
    /************************ Methods ****************************************/
  } // end of interface - ICommentProviderData


  /************************** IProvider **************************************/
  /// <summary>
  /// Describes the basic provider for the <see cref="CommentMyCode"/> context
  /// </summary>
  public interface IProvider
  {
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /// <summary>
    /// Gets the main context that initialized us
    /// </summary>
    IItemContext Context { get; }
    /************************ Methods ****************************************/
    /// <summary>
    /// Initializes the provider with a given context
    /// </summary>
    /// <param name="context"></param>
    void Initialize(IItemContext context);
  } // end of interface - IProvider


}


/* End IProvider.cs */