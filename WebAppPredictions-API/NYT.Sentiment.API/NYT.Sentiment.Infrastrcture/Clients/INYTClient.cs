using SentimentRazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYT.Sentiment.Infrastrcture.Clients
{
    public interface INYTClient
    {
        Task<JsonBodyResponse> GetNYTArticlesWithQuery(string query, string apiKey, string startDate, string endDate);
    }
}
