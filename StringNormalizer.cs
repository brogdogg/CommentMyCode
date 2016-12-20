/******************************************************************************
 * File...: StringNormalizer.cs
 * Remarks:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /************************ Construction ***********************************/
    /*----------------------- StringNormalizer ------------------------------*/
    /// <summary>
    /// Creates a new string normalizer with a maximum column count
    /// </summary>
    /// <param name="maxColCnt">
    /// Optional maximum column count for a word boundary, default is 80
    /// </param>
    public StringNormalizer(int maxColCnt = 80)
    {
      MaxColumnCount = maxColCnt;
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
    {
      return Normalize(strToNormalize, 0);
    } // end of function - Normalize

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
      if (offset < 0 || offset >= MaxColumnCount)
        throw new InvalidOperationException("Offset is outside the operating range");

      // If we have a non-zero/non-null string, we will process and normalize
      // with respect to the word boundary
      if (strToNormalize != null && strToNormalize.Length > 0)
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
      else if (strToNormalize != null)
        yield return strToNormalize;
    } // end of function - Normalize
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

}


/* End StringNormalizer.cs */
