# OpenGraphIO ( Net Client 1.0.0 )

[OpenGraph.io](https://www.opengraph.io/) client library for [.net](https://dotnet.microsoft.com/en-us/). Given a URL, the client
will make a HTTP request to OpenGraph.io which will scrape the site for OpenGraph tags. If tags exist the tags will
be returned to you.

If some tags are missing, the client will attempt to infer them from the content on the page, and these inferred tags will be returned as part of the `hybridGraph`.

The `hybridGraph` results will always default to any OpenGraph tags that were found on the page. If only some tags
were found, or none were, the missing tags will be inferred from the content on the page.

To get a free forever key, signup at [OpenGraph.io](https://www.opengraph.io/).

The vast majority of projects will be totally covered using one of our inexpensive plans.  
Dedicated plans are also available upon request.

## Usage

The library provides the OpenGraphIO class, which can be instantiated with the required options. Here's an example:

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Replace "YOUR_APP_ID" with your actual OpenGraph.io App ID.
        var options = new OpenGraphIOOptions
        {
            AppId = "YOUR_APP_ID",
            CacheOk = true,
            // Add other options as needed.
        };

        var ogClient = new OpenGraphIO(options);

        var url = "https://www.example.com"; // Replace with the URL you want to retrieve Open Graph data for.

        try
        {
            string result = await ogClient.GetSiteInfo(url);
            Console.WriteLine(result); // Output the Open Graph data as a JSON string.
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
        }
    }
}
```

## OpenGraphIOOptions

The OpenGraphIOOptions class allows you to specify various parameters when making requests to the API. The available options include:

- **AppId**: Your OpenGraph.io App ID (required).
- **Service**: The service to be used (default: "site").
- **Version**: The API version to use (default: "1.1").
- **CacheOk**: A boolean indicating whether to use cached results for quickness (default: true).
- **UseProxy**: A boolean indicating whether to route requests through proxies to avoid bot detection (default: false).
- **FullRender**: A boolean indicating whether to fully render the site using a browser for JS-dependent sites (default: false).
- **MaxCacheAge**: The maximum cache age in milliseconds (default: 5 days).
- **AcceptLang**: The request language sent when requesting the URL (default: "en-US,en;q=0.9 auto").
- **HtmlElements**: An optional parameter specifying the HTML elements you want to extract from the website.

The options shown above are the default options. To understand more about these parameters, please view our documentation at: https://www.opengraph.io/documentation/

The options supplied to the constructor above will be applied to any requests made by the library but can be overridden
by supplying parameters at the time of calling `getSiteInfo`.

## OpenGraphIO Services

Service Options: `site`, `extract`, `scrape`

### Site Service

Unleash the power of our Unfurling API to effortlessly extract Open Graph tags from any URL.

#### Options

| Parameter   | Required | Example             | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| ----------- | -------- | ------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| appId       | yes      | -                   | The API key for registered users. Create an account (no cc ever required) to receive your `app_id`.                                                                                                                                                                                                                                                                                                                                                                                                                         |
| cacheOk     | no       | false               | This will force our servers to pull a fresh version of the site being requested. By default, this value is true.                                                                                                                                                                                                                                                                                                                                                                                                            |
| fullRender  | no       | false               | This will fully render the site using a chrome browser before parsing its contents. This is especially helpful for single page applications and JS redirects. This will slow down the time it takes to get a response by around 1.5 seconds.                                                                                                                                                                                                                                                                                |
| useProxy    | no       | false               | Route your request through residential and mobile proxies to avoid bot detection. This will slow down requests 3-10 seconds and can cause requests to time out. NOTE: Proxies are a limited resource and expensive for our team maintain. Free accounts share a small pool of proxies. If you plan on using proxies often, paid accounts provide dedicated concurrent proxies for your account.                                                                                                                             |
| maxCacheAge | no       | 432000000           | This specifies the maximum age in milliseconds that a cached response should be. If not specified, the value is set to 5 days. (5 days _ 24 hours _ 60 minutes _ 60 seconds _ 1000ms = 432,000,000 ms)                                                                                                                                                                                                                                                                                                                      |
| acceptLang  | no       | en-US,en;q=0.9 auto | This specifies the request language sent when requesting the URL. This is useful if you want to get the site for languages other than English. The default setting for this will return an English version of a page if it exists. Note: if you specify the value auto, the API will use the same language settings as your current request. For more information on what to supply for this field, please see: [Accept-Language - MDN Web Docs](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Accept-Language) |

### Extract Service

The extract endpoint enables you to extract information from any website by providing its URL. With this endpoint, you can extract any element you need from the website, including but not limited to the title, header elements (h1 to h5), and paragraph elements (p).

#### Options

| Parameter    | Required | Example             | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| ------------ | -------- | ------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| appId        | yes      | -                   | The API key for registered users. Create an account (no cc ever required) to receive your `app_id`.                                                                                                                                                                                                                                                                                                                                                                                                                         |
| htmlElements | no       | h1,h2,p             | This is an optional parameter and specifies the HTML elements you want to extract from the website. The value should be a comma-separated list of HTML element names. If this parameter is not supplied, the default elements that will be extracted are h1, h2, h3, h4, h5, p, and title.                                                                                                                                                                                                                                  |
| cacheOk      | no       | false               | This will force our servers to pull a fresh version of the site being requested. By default, this value is true.                                                                                                                                                                                                                                                                                                                                                                                                            |
| fullRender   | no       | false               | This will fully render the site using a chrome browser before parsing its contents. This is especially helpful for single page applications and JS redirects. This will slow down the time it takes to get a response by around 1.5 seconds.                                                                                                                                                                                                                                                                                |
| useProxy     | no       | false               | Route your request through residential and mobile proxies to avoid bot detection. This will slow down requests 3-10 seconds and can cause requests to time out. NOTE: Proxies are a limited resource and expensive for our team maintain. Free accounts share a small pool of proxies. If you plan on using proxies often, paid accounts provide dedicated concurrent proxies for your account.                                                                                                                             |
| maxCacheAge  | no       | 432000000           | This specifies the maximum age in milliseconds that a cached response should be. If not specified, the value is set to 5 days. (5 days _ 24 hours _ 60 minutes _ 60 seconds _ 1000ms = 432,000,000 ms)                                                                                                                                                                                                                                                                                                                      |
| acceptLang   | no       | en-US,en;q=0.9 auto | This specifies the request language sent when requesting the URL. This is useful if you want to get the site for languages other than English. The default setting for this will return an English version of a page if it exists. Note: if you specify the value auto, the API will use the same language settings as your current request. For more information on what to supply for this field, please see: [Accept-Language - MDN Web Docs](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Accept-Language) |

### Scrape Service

Just need the raw HTML?
The Scrape Site endpoint is used to scrape the HTML of a website given its URL

#### Options

| Parameter   | Required | Example             | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| ----------- | -------- | ------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| appId       | yes      | -                   | The API key for registered users. Create an account (no cc ever required) to receive your `app_id`.                                                                                                                                                                                                                                                                                                                                                                                                                         |
| cacheOk     | no       | false               | This will force our servers to pull a fresh version of the site being requested. By default, this value is true.                                                                                                                                                                                                                                                                                                                                                                                                            |
| fullRender  | no       | false               | This will fully render the site using a chrome browser before parsing its contents. This is especially helpful for single page applications and JS redirects. This will slow down the time it takes to get a response by around 1.5 seconds.                                                                                                                                                                                                                                                                                |
| useProxy    | no       | false               | Route your request through residential and mobile proxies to avoid bot detection. This will slow down requests 3-10 seconds and can cause requests to time out. NOTE: Proxies are a limited resource and expensive for our team maintain. Free accounts share a small pool of proxies. If you plan on using proxies often, paid accounts provide dedicated concurrent proxies for your account.                                                                                                                             |
| maxCacheAge | no       | 432000000           | This specifies the maximum age in milliseconds that a cached response should be. If not specified, the value is set to 5 days. (5 days _ 24 hours _ 60 minutes _ 60 seconds _ 1000ms = 432,000,000 ms)                                                                                                                                                                                                                                                                                                                      |
| acceptLang  | no       | en-US,en;q=0.9 auto | This specifies the request language sent when requesting the URL. This is useful if you want to get the site for languages other than English. The default setting for this will return an English version of a page if it exists. Note: if you specify the value auto, the API will use the same language settings as your current request. For more information on what to supply for this field, please see: [Accept-Language - MDN Web Docs](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Accept-Language) |

## Support

Feel free to reach out at any time with questions or suggestions by adding to the issues for this repo or if you'd
prefer, head over to [https://www.opengraph.io/support/](https://www.opengraph.io/support/) and drop us a line!

## License

MIT License

Copyright (c) Opengraph.io

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
