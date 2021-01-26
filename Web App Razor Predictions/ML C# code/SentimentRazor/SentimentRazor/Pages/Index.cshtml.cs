using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
using SentimentRazor.Models;
using SentimentRazorML.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace SentimentRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;
        private readonly ApplicationDbContext _db;
        private readonly IHttpClientFactory _httpclientFactory;

        public IndexModel(PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool, ApplicationDbContext db, IHttpClientFactory httpclientfactory)
        {
            _predictionEnginePool = predictionEnginePool;
            _db = db;
            _httpclientFactory = httpclientfactory;
        }

        [BindProperty] // if we didnt have this line we would need to add a parameter to OnPost
        public SentimentResult SentimentResult { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid) //checker if the requiered properties are being added in the front end
            {
                await _db.SavePrediction.AddAsync(SentimentResult);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                Console.WriteLine(errors);
                return Page();


            }
        }

        private static int RoundValueToNext100(double value)
        {
            return (int)(Math.Ceiling(value / 100) * 100);
        }

        public IActionResult OnGetAnalyzeSentiment([FromQuery] string text)
        {
            if (String.IsNullOrEmpty(text)) return Content("Sentiment");
            var input = new ModelInput { CleanText = text };
            var prediction = _predictionEnginePool.Predict(input);
            var sentiment = prediction.Prediction;
            var confScore = prediction.Score[0];
            var roundedConfScore=String.Format("{0:0.00}", confScore);
            return Content("Sentiment: " + sentiment + " Confidence Score: " + roundedConfScore.ToString());
        }

        public async Task<IActionResult> OnGetArticles()
        {

            HttpResponseMessage resp;

            var req = new HttpRequestMessage(HttpMethod.Get, "https://api.nytimes.com/svc/search/v2/articlesearch.json?q=immigration=&api-key=");
            //req.Headers.Add("Accept", "application/x-www-form-urlencoded");
            //req.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            // This is the important part:
        //    req.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        //{
        //    { "q", "immigration" },
        //    { "api-key", "a0HA3uBISDkGyvUGR3FeoAGybtDVPPM5" }
        //   });


            var client = _httpclientFactory.CreateClient();
            resp = await client.SendAsync(req);
            var jsonString = await resp.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<myItem>(jsonString);
            var response1= result;

            return Content(response1.ToString());
        }


        //_httpclient.BaseAddress = new Uri("");
        //_httpclient.DefaultRequestHeaders.Accept.Clear();
        //_httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    }

public class myItem
{
    [JsonProperty("response")]
    public Response response { get; set; }
}

public class Response
{
    [JsonProperty("docs")]
    public List<Docs> docs;
}

public class Docs
{
    public string lead_paragraph;
}


