namespace ProxySharp.Parsers
{
    /// <summary>
    /// <see cref="HtmlTableParser"/> configuration.
    /// </summary>
    public class TableParserConfiguration
	{
		/// <summary>
		/// Table absolute selector.
		/// </summary>
		/// <remarks>For example, "#list > div > div.table-responsive > div > table".</remarks>
		public string TableSelector { get; set; } = "table";

		/// <summary>
		/// Header tag.
		/// </summary>
		/// <remarks>Default "thead".</remarks>
		public string HeaderSelector { get; set; } = "thead";

		/// <summary>
		/// Body tag.
		/// </summary>
		/// <remarks>Default "tbody".</remarks>
		public string BodySelector { get; set; } = "tbody";

		/// <summary>
		/// Column header tag.
		/// </summary>
		/// <remarks>Default "th".</remarks>
		public string ColumnHeaderSelector { get; set; } = "th";
	}
}
