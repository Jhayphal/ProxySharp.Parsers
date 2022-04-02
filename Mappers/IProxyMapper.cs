using System.Collections.Generic;

namespace ProxySharp.Parsers.Mappers
{
    /// <summary>
    /// Map table to the specific type.
    /// </summary>
    /// <typeparam name="TResult">List rows.</typeparam>
    public interface IProxyMapper<TResult>
        where TResult : new()
    {
        /// <summary>
        /// Map table rows to properties of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <param name="columns">List tables column names.</param>
        /// <param name="rows">Table rows.</param>
        /// <returns>Rows collection.</returns>
        /// <remarks>Properties of type <typeparamref name="TResult"/> should be named as columns without spaces. Case ignores.</remarks>
        IEnumerable<TResult> Map(IList<string> columns, IEnumerable<IList<string>> rows);
    }
}
