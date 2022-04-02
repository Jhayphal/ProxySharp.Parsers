using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProxySharp.Parsers.Mappers
{
    public sealed class ProxyMapper<TResult> : IProxyMapper<TResult> where TResult : new()
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="columns"><inheritdoc/></param>
        /// <param name="rows"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        /// <remarks><inheritdoc/> Properties should be a string type.</remarks>
        public IEnumerable<TResult> Map(IList<string> columns, IEnumerable<IList<string>> rows)
        {
            var type = typeof(TResult);
            var properties = type.GetProperties();

            var recode = CreateIndex(properties, columns);

            foreach (var row in rows)
            {
                var item = new TResult();

                for (int i = 0; i < row.Count; ++i)
                {
                    if (recode.TryGetValue(i, out var property))
                        property.SetValue(item, row[i].Trim());
                }

                yield return item;
            }
        }

        private Dictionary<int, PropertyInfo> CreateIndex(IEnumerable<PropertyInfo> properties, IList<string> columns)
        {
            var result = new Dictionary<int, PropertyInfo>();

            for (int i = 0; i < columns.Count; ++i)
            {
                var currentName = columns[i]
                    .Trim()
                    .Replace(" ", string.Empty)
                    .ToLower();

                var property = properties
                    .FirstOrDefault(p => p.Name.ToLower() == currentName);

                if (property != null)
                    result.Add(i, property);
            }

            return result;
        }
    }
}
