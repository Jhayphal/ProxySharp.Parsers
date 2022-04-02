using ProxySharp.Parsers.Mappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProxySharp.Parsers
{
    /// <summary>
    /// Parse an HTML page with the proxies list.
    /// </summary>
    public interface IHtmlTableParser
    {
        /// <summary>
        /// Parse list of proxies.
        /// </summary>
        /// <typeparam name="TResult">Type of results.</typeparam>
        /// <param name="pageContent">Html-page content.</param>
        /// <param name="mapper">Mapper to use.</param>
        /// <returns>Collection of items.</returns>
        Task<IList<TResult>> ParseAsync<TResult>(string pageContent, IProxyMapper<TResult> mapper) where TResult : new();
    }
}
