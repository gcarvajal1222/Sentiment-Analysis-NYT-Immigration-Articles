using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NYT.Sentiment.Infrastrcture.Clients;
using SentimentRazor.Models;

namespace NYT.Sentiment.API.Controllers
{
    public class NYTController : Controller
    {
        // GET: NYTController
        private readonly INYTClient _nytTimesClient;

        public NYTController(INYTClient nytTimesClient)
        {
            _nytTimesClient = nytTimesClient;
        }


        /// <summary>
        /// Gets NYT articles.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetArticlesFromNYT")]
        public async Task<ActionResult<JsonBodyResponse>> GetArticlesFromNYT(string query, string apiKey, string startDate, string endDate)
        {
            var res = await _nytTimesClient.GetNYTArticlesWithQuery(query, apiKey, startDate, endDate);
            return View(res);
        }

        // POST: NYTController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
