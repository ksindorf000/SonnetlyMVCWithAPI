using Microsoft.AspNet.Identity;
using SonnetlyMVCWithAPI.Helpers;
using SonnetlyMVCWithAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SonnetlyMVCWithAPI.Controllers
{
    public class SonnetViewController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        internal SonnetRepo repo;

        public SonnetViewController()
        {
            repo = new SonnetRepo(db);
        }

        // Index: all sonnets
        [Route("s/All")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.sonnetList = repo.GetSonnets(userId);
            return View();
        }

        //[HttpPost]
        //public async Task<ActionResult> CreateProduct(Sonnet model)
        //{
        //    var response = await productClient.CreateProduct(model);
        //    var productId = response.Data;
        //    return RedirectToAction("GetProduct", new { id = productId });
        //}
    }
}
