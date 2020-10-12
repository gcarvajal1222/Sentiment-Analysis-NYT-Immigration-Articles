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

namespace SentimentRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;
        private readonly ApplicationDbContext _db;

        public IndexModel(PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool, ApplicationDbContext db)
        {
            _predictionEnginePool = predictionEnginePool;
            _db = db;
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
    }
}
