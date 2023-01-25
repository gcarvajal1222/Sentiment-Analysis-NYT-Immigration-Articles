using Newtonsoft.Json;
using SentimentRazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NYT.Sentiment.Infrastrcture.Clients
{

    
    public class NYTClient : INYTClient
    {
        private readonly IHttpClientFactory? _httpclientFactory;
        public NYTClient(IHttpClientFactory httpclientfactory)
        {
            _httpclientFactory = httpclientfactory;
        }

        public async Task<JsonBodyResponse> GetNYTArticlesWithQuery(string query,string apiKey,string startDate,string endDate)
        {

                HttpResponseMessage resp;

                var req = new HttpRequestMessage(HttpMethod.Get, "https://api.nytimes.com/svc/search/v2/articlesearch.json?q=" + query + "=&api-key=" + apiKey + "&begin_date=" + startDate + "&end_date=" + endDate + "&sort =oldest");
                var client = _httpclientFactory.CreateClient();
                resp = await client.SendAsync(req);
                var jsonString = await resp.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseAPI>(jsonString);
                var countResponse = result.response.meta.hits;
                var allDocs = result.response.docs;
                var obj = new JsonBodyResponse { ArticlesCounts = countResponse.ToString(), response = allDocs };
                return obj;

            }
        }

    }
