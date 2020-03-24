using System.Xml.Linq;

namespace CCLLC.CDS.Sdk
{ 
    /// <summary>
    /// Provides a mechanism to retrieve XML configuration data stored as XML web resources.
    /// </summary>
    public interface IXmlConfigurationResource
    {
        /// <summary>
        /// Retrieve a named resource as an <see cref="XDocument"/> with optional caching of
        /// the resource. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        XDocument Get(string key, bool useCache = true);
    }
}
