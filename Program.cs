using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web;

public class OpenGraphIO
{
    private OpenGraphIOOptions options;

    public OpenGraphIO(OpenGraphIOOptions options)
    {
        this.options = options ?? new OpenGraphIOOptions();
        this.options.CacheOk = this.options.CacheOk ?? true;
        this.options.Service = this.options.Service ?? "site";
        this.options.Version = this.options.Version ?? "1.1";

        if (string.IsNullOrWhiteSpace(this.options.AppId))
        {
            throw new Exception("appId must be supplied when making requests to the API. Get a free appId by signing up here: https://www.opengraph.io/");
        }
    }

    private string GetSiteInfoUrl(string url)
    {
        var encodedUrl = HttpUtility.UrlEncode(url);
        string apiBaseUrl = "https://opengraph.io/api/";

        return $"{apiBaseUrl}{this.options.Version}/{this.options.Service}/{encodedUrl}";
    }

    private Dictionary<string, string> GetSiteInfoQueryParams(OpenGraphIOOptions options)
    {
        var queryStringValues = new Dictionary<string, string>();
        queryStringValues["cache_ok"] = options.CacheOk.HasValue && options.CacheOk.Value ? "true" : "false";
        queryStringValues["use_proxy"] = options.UseProxy.HasValue && options.UseProxy.Value ? "true" : "false";

        if (!string.IsNullOrWhiteSpace(options.AppId)) queryStringValues["app_id"] = options.AppId;
        if (options.FullRender.HasValue && options.FullRender.Value) queryStringValues["full_render"] = "true";
        if (options.MaxCacheAge.HasValue) queryStringValues["max_cache_age"] = options.MaxCacheAge.Value.ToString();
        if (!string.IsNullOrWhiteSpace(options.AcceptLang)) queryStringValues["accept_lang"] = options.AcceptLang;
        if (!string.IsNullOrWhiteSpace(options.HtmlElements)) queryStringValues["html_elements"] = options.HtmlElements;

        return queryStringValues;
    }

    public async Task<string> GetSiteInfo(string url, OpenGraphIOOptions? options = null)
    {
        // If no options provided, use the instance options
        options = options ?? this.options;

        // Get the base URL for the API request
        string baseUrl = GetSiteInfoUrl(url);


        // Get the query string values for the API request
        var queryStringValues = GetSiteInfoQueryParams(options);

        using (HttpClient client = new HttpClient())
        {
            // Build URL with query parameters
            var query = new FormUrlEncodedContent(queryStringValues);
            var response = await client.GetAsync(baseUrl + "?" + await query.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}

public class OpenGraphIOOptions
{
    public string AppId { get; set; }
    public string? Service { get; set; }
    public string? Version { get; set; }
    public bool? CacheOk { get; set; }
    public bool? UseProxy { get; set; }
    public bool? FullRender { get; set; }
    public int? MaxCacheAge { get; set; }
    public string? AcceptLang { get; set; }
    public string? HtmlElements { get; set; }
    // Add other properties as necessary
}


