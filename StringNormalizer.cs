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

      if (strToNormalize != null)
      {
        var strCol = strToNormalize.Split(' ');
        var retval = "";
        int curLen = offset;
        foreach(var str in strCol)
        {
          if ((curLen = curLen + str.Length) > MaxColumnCount)
          {
            curLen = offset;
            yield return retval;
            retval = "";
          }
          else
            retval += curLen == 0 ? str : " " + str;
        } // end of foreach - string in the split collection
        if (curLen > 0)
          yield return retval;
      } // end of if - valid string to normalize
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
