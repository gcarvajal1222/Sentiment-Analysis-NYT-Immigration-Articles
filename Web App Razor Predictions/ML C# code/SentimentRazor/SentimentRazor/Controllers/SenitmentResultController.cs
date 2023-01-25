using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SentimentRazor.Models;
using Microsoft.EntityFrameworkCore;

namespace SentimentRazor.Controllers
{
    [Route("api/Sentiment")]
    [ApiController]
    public class SenitmentResultController : Controller
    {

        private readonly ApplicationDbContext _db;

        public SenitmentResultController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.SavePrediction.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var SavePredictionFromDb = await _db.SavePrediction.FirstOrDefaultAsync(u => u.Id == id);
            if (SavePredictionFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.SavePrediction.Remove(SavePredictionFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }

    }
}
