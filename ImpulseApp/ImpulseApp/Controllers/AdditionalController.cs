using ImpulseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ImpulseApp.DBService;

namespace ImpulseApp.Controllers
{
    public class AdditionalController : Controller
    {
        //
        // GET: /Additional/
        ApplicationDbContext context = new ApplicationDbContext();
        IDBService db = new DBServiceClient();
        public ActionResult GetCurrentUserAds(int AdId = -1)
        {
            string id = User.Identity.GetUserId();
            var ads = db.GetUserAds(id).Where(a => a.Id != AdId);
            ViewBag.Ads = new SelectList(ads, "Id", "Name");
            return PartialView("GetCurrentUserAds");
        }

        public ActionResult GenerateAds()
        {
            var ads = db.GetUserAds(User.Identity.GetUserId());
            ViewBag.AdId = new SelectList(ads, "Id", "Id");
            return View();
        }
        [HttpPost]
        public ActionResult GenerateAds(int AdId, DateTime BeginDate, DateTime EndDate)
        {
            StubService.IStubService stub = new StubService.StubServiceClient();
            stub.GenerateStats(AdId, BeginDate.ToShortDateString(), EndDate.ToShortDateString());
            return RedirectToAction("StatisticsIndex", "UserFront");
        }
	}
}