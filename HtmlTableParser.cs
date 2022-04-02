using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using ProxySharp.Parsers.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProxySharp.Parsers
{
    public class HtmlTableParser : IHtmlTableParser
    {
        public readonly TableParserConfiguration Configuration;

        private readonly HtmlParser _parser = new HtmlParser();

        public HtmlTableParser(TableParserConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<IList<TResult>> ParseAsync<TResult>(string pageContent, IProxyMapper<TResult> mapper)
            where TResult : new()
        {
            var table = await GetTableAsync(pageContent);

            var columns = ParseHeader(table);

            var body = table.QuerySelector(Configuration.BodySelector);

            if (body == null)
                throw new ArgumentNullException(nameof(body));

            var data = new List<List<string>>();
            var rows = body.Children;

            foreach (var row in rows)
            {
                var current = new List<string>();

                foreach (var cell in row.Children)
                    current.Add(cell.TextContent);

                data.Add(current);
            }

            return mapper.Map(columns, data)
                .ToList();
        }

        private async Task<IElement> GetTableAsync(string pageContent)
        {
            var document = await _parser.ParseDocumentAsync(pageContent);

            var messages = document.QuerySelector(Configuration.TableSelector);

            if (messages == null)
                throw new InvalidOperationException();

            return messages;
        }

        private List<string> ParseHeader(IElement table)
        {
            var header = table.QuerySelector(Configuration.HeaderSelector);

            if (header == null)
                throw new ArgumentNullException(nameof(header));

            var columns = header.QuerySelectorAll(Configuration.ColumnHeaderSelector);

            if (columns == null)
                throw new ArgumentNullException(nameof(columns));

            var result = new List<string>();

            foreach (var column in columns)
                result.Add(column.TextContent.Trim());

            return result;
        }
    }
}
