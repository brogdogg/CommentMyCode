/******************************************************************************
 * File...: IProvider.cs
 * Remarks:
 */

namespace MB.VS.Extension.CommentMyCode.Provider
{


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
    CommentMyCode Context { get; }
    /************************ Methods ****************************************/
    /// <summary>
    /// Initializes the provider with a given context
    /// </summary>
    /// <param name="context"></param>
    void Initialize(CommentMyCode context);
  } // end of interface - IProvider


  /************************** ICommentProvider *******************************/
  /// <summary>
  /// 
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

    /// <summary>
    /// Comments a class
    /// </summary>
    void CommentClass();

    /// <summary>
    /// Comments an enum
    /// </summary>
    void CommentEnum();

    /// <summary>
    /// Comments a file
    /// </summary>
    void CommentFile();

    /// <summary>
    /// Comments a function
    /// </summary>
    void CommentFunction();

    /// <summary>
    /// Comments a property
    /// </summary>
    void CommentProperty();
  } // end of interface - ICommentProvider


}


/* End IProvider.cs */