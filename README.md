# ProxySharp.Parsers

Provides functionality for parsing proxies lists from the websites. The list of proxy servers could be used by the ProxySharp package.

## Usage expamle

Declare class with properties named as columns (without spaces, case insensitive).

```
class FreeProxyList
{
  public string IpAddress { get; set; }
  public string Port { get; set; }
  public string Google { get; set; }
  public string Https { get; set; }
  public string LastChecked { get; set; }
}
```

Parse web-page to a proxies list.

```
var client = new HttpClient();
var page = await client.GetAsync(ProxiesUrl);
var content = await page.Content.ReadAsStringAsync();
			
var config = new TableParserConfiguration
{
  TableSelector = "#list > div > div.table-responsive > div > table"
};

var parser = new HtmlTableParser(config);
var mapper = new ProxyMapper<FreeProxyList>();

var items = await parser.ParseAsync(content, mapper);

var proxies = items
  .Where(x => int.TryParse(x.Port, out var _))
  .Select(x => new ProxyInfo
  {
    Host = x.IpAddress,
    Port = int.Parse(x.Port)
  });

foreach (var proxy in proxies)
  Console.WriteLine(proxy);
```