using ImpulseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ImpulseApp.Controllers
{
    public class AdditionalController : Controller
    {
        //
        // GET: /Additional/
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult GetCurrentUserAds(int AdId = -1)
        {
            string id = User.Identity.GetUserId();
            var ads = context.SimpleAds.Where(a => a.UserId.Equals(id) && a.Id!=AdId);
            ViewBag.Ads = new SelectList(ads, "Id", "DateTime");
            return PartialView("GetCurrentUserAds");
        }
	}
}