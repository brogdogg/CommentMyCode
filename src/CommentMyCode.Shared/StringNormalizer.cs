/******************************************************************************
 * File...: StringNormalizer.cs
 * Remarks:
 */
using System;
using System.Collections.Generic;

namespace MB.VS.Extension.CommentMyCode
{


  /************************** StringNormalizer *******************************/
  /// <summary>
  /// Normalizes a string, making sure it is only a specified characters long
  /// creating new strings containing words that go over the boundary
  /// </summary>
  public class StringNormalizer
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /*----------------------- CloseCommentStr -------------------------------*/
    /// <summary>
    /// Gets/Sets the close comment string
    /// </summary>
    public string CloseCommentStr
    {
      get;
      set;
    } = null; // end of property - CloseCommentStr

    /*----------------------- MaxColumnCount --------------------------------*/
    /// <summary>
    /// Gets the maximum column count, where the word boundary is, any words
    /// beyond this boundary will be inserted into a new line
    /// </summary>
    public int MaxColumnCount
    {
      get;
      protected set;
    } // end of property - MaxColumnCount

    /*----------------------- OpenCommentStr --------------------------------*/
    /// <summary>
    /// Gets/Sets the open comment string
    /// </summary>
    public string OpenCommentStr
    {
      get;
      set;
    } = null; // end of property - OpenCommentStr


    /************************ Construction ***********************************/
    /*----------------------- StringNormalizer ------------------------------*/
    /// <summary>
    /// Creates a new string normalizer with a maximum column count
    /// </summary>
    /// <param name="maxColCnt">
    /// Optional maximum column count for a word boundary, default is 80
    /// </param>
    public StringNormalizer(
      int maxColCnt = 80,
      string openCommentStr = null,
      string closeCommentStr = null)
    {
      CloseCommentStr = closeCommentStr;
      MaxColumnCount = maxColCnt;
      OpenCommentStr = openCommentStr;
      return;
    } // end of function - StringNormalizer


    /************************ Methods ****************************************/
    /*----------------------- Normalize -------------------------------------*/
    /// <summary>
    /// Normalizes a string, if any words go beyond the specified
    /// <see cref="MaxColumnCount"/> then a new string will be created with the
    /// words remaining
    /// </summary>
    /// <param name="strToNormalize">
    /// The string to normalize
    /// </param>
    /// <returns>
    /// A collection of normalized strings to be within the word boundary
    /// </returns>
    /// <remarks>
    /// This method uses an offset of zero
    /// </remarks>
    public virtual IEnumerable<string> Normalize(string strToNormalize)
      => Normalize(strToNormalize, 0);


    /*----------------------- Normalize -------------------------------------*/
    /// <summary>
    /// Normalizes a string, if any words go beyond the specified
    /// <see cref="MaxColumnCount"/> then a new string will be created with the
    /// words remaining
    /// </summary>
    /// <param name="strToNormalize">
    /// The string to normalize
    /// </param>
    /// <param name="offset">
    /// The offset to use when normalizing the string
    /// </param>
    /// <returns>
    /// A collection of normalized strings to be within the word boundary
    /// </returns>
    public virtual IEnumerable<string> Normalize(string strToNormalize, int offset)
    {
      if (strToNormalize == null)
        throw new ArgumentNullException("String to normalize must not be null");

      if (offset < 0 || offset >= MaxColumnCount)
        throw new InvalidOperationException("Offset is outside the operating range");

      // If we have a non-zero/non-null string, we will process and normalize
      // with respect to the word boundary
      if (strToNormalize.Length > 0)
      {
        var strCol = strToNormalize.Split(' ');
        var retval = "";
        bool isBegin = true;

        // Go through each word in the input string
        foreach (var str in strCol)
        {
          // Are we at the beginning of the input string?
          if(isBegin)
          {
            retval += str;
            isBegin = false;
          } // end of if - is the beginning of the string
          // If the combination of strings will go ver the max, then we will
          // return what we have and start a new string
          // Notice the +1, this is for the space added between words
          else if ((retval.Length + str.Length + 1 + offset) >= MaxColumnCount)
          {
            yield return retval;
            retval = str;
          } // end of if - reached max word boundary
          // Else we are still within the boundary, keep building the string
          else
            retval += " " + str;
        } // end of foreach - string in the split collection

        // If our return value has a valid length of data, then we will yield
        // up the leftovers
        if (retval.Length > 0)
          yield return retval;

      } // end of if - valid string to normalize
      // If the input is not null then it must be blank, we will just yield it
      else
        yield return strToNormalize;
    } // end of function - Normalize

    /*----------------------- NormalizeSingleCommentStr ---------------------*/
    /// <summary>
    /// Normalizes a string assumed to be a single line comment, taking into
    /// account the <see cref="CloseCommentStr"/> string when normalizing
    /// </summary>
    /// <param name="strToNormalize">
    /// String to normalize
    /// </param>
    /// <returns></returns>
    public virtual string NormalizeSingleCommentStr(string strToNormalize)
    {
      var retval = strToNormalize;
      var closeCommentStrLen = CloseCommentStr == null ? 0 : CloseCommentStr.Length;
      if(strToNormalize.Length > MaxColumnCount)
      {
        var tmp = strToNormalize.Substring(0, MaxColumnCount - 4 - closeCommentStrLen - 1);
        retval = tmp + "... " + CloseCommentStr;
      }
      return retval;
    } // end of function - NormalizeSingleCommentStr
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
  } // end of class - StringNormalizer
} // end of namespace - MB.VS.Extension.CommentMyCode
/* End StringNormalizer.cs */
