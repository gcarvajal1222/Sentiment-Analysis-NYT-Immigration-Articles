using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NYT.Sentiment.API.Controllers
{
    public class NYTController : Controller
    {
        // GET: NYTController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NYTController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NYTController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NYTController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NYTController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NYTController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NYTController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NYTController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
