using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SentimentRazor.Models;
using Microsoft.EntityFrameworkCore;

namespace SentimentRazor.Pages
{
    public class SentimentDbModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public SentimentDbModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<SentimentResult> SentimentResults { get; set; }

        public async Task OnGet()
        {
            SentimentResults = await _db.SavePrediction.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var prediction = await _db.SavePrediction.FindAsync(id);
            if (prediction == null)
            {
                return NotFound();
            }
            _db.SavePrediction.Remove(prediction);
            await _db.SaveChangesAsync();

            return RedirectToPage("SentimentDb");
        }

        //public async Task<IActionResult> OnGetEdit(int? id)
        //{
        //    if (id==null)
        //    {
        //        return NotFound();
        //    }
        //    var prediction = await _db.SavePrediction.FindAsync(id);
        //    if (prediction==null)
        //    {
        //        return NotFound();
        //    }
        //    return ViewComponent(prediction.ToString());
        //}


        //public async Task<IActionResult> OnPosEdit(int id)
        //{
        //    var prediction = await _db.SavePrediction.FindAsync(id);
        //    if (prediction == null)
        //    {
        //        return NotFound();
        //    }
        //    _db.SavePrediction.Update(prediction);
        //    await _db.SaveChangesAsync();

        //    return RedirectToPage("SentimentDb");
        //}
    }
}