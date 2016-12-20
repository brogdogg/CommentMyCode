/******************************************************************************
 * File...: ProviderFactory.cs
 * Remarks:
 */
using MB.VS.Extension.CommentMyCode.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.VS.Extension.CommentMyCode.Providers
{


  /************************** ProviderFactory ********************************/
  /// <summary>
  /// 
  /// </summary>
  public class ProviderFactory
  {
    /*======================= PUBLIC ========================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /*----------------------- BuildProvider ---------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    public virtual ICommentProvider BuildProvider(IItemContext context)
    {
      var retval = m_providers
        .Where(v => (v.Metadata.SupportedCommandTypes & (int)context.CommentType) != 0
                  && v.Metadata.SupportedExtension == context.Extension)
        .SingleOrDefault();
      if (retval != null && !retval.IsValueCreated)
        retval.Value.Initialize(context);
      return retval == null ? null : retval.Value;
    } // end of function - BuildProvider

    /************************ Fields *****************************************/
    /************************ Static Properties ******************************/
    /*----------------------- Instance --------------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    public static ProviderFactory Instance
    {
      get { return INSTANCE; }
    } // end of property - Instance
    /************************ Static *****************************************/
    /*----------------------- Initialize ------------------------------------*/
    /// <summary>
    /// Initializes an instance of the factory with a default catalog for
    /// finding parts, based on the same assembly this class is located in
    /// </summary>
    /// <returns></returns>
    public static ProviderFactory Initialize()
    {
      AggregateCatalog catalog = new AggregateCatalog();
      catalog.Catalogs.Add(new AssemblyCatalog(typeof(ProviderFactory).Assembly));
      return Initialize(catalog);
    } // end of function - Initialize

    /*----------------------- Initialize ------------------------------------*/
    /// <summary>
    /// Initializes the instance of the <see cref="ProviderFactory"/> with the
    /// specified catalog for part location
    /// </summary>
    /// <returns></returns>
    public static ProviderFactory Initialize(AggregateCatalog catalog)
    {
      if (INSTANCE != null)
        throw new InvalidOperationException("Provider factory is already initialized");

      return (INSTANCE = new ProviderFactory(catalog));
    } // end of function - Initialize


    /*======================= PROTECTED =====================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /*----------------------- ProviderFactory -------------------------------*/
    /// <summary>
    /// 
    /// </summary>
    protected ProviderFactory(AggregateCatalog catalog)
    {
      if (null == catalog)
        throw new ArgumentNullException("Invalid catalog specified");
      m_container = new CompositionContainer(catalog);
      try
      {
        m_container.ComposeParts(this);
      }
      catch(CompositionException exc)
      {
        Debug.WriteLine("Failed to compose parts: " + exc.ToString());
      }
      return;
    } // end of function - ProviderFactory

    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    /************************ Static *****************************************/

    /*======================= PRIVATE =======================================*/
    /************************ Events *****************************************/
    /************************ Properties *************************************/
    /************************ Construction ***********************************/
    /************************ Methods ****************************************/
    /************************ Fields *****************************************/
    private CompositionContainer m_container = null;
    [ImportMany]
    private IEnumerable<Lazy<ICommentProvider, ICommentProviderData>> m_providers = null;
    /************************ Static *****************************************/
    private static ProviderFactory INSTANCE = null;
  } // end of class - ProviderFactory


}


/* End ProviderFactory.cs */