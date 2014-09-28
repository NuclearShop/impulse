using ImpulseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImpulseApp.Controllers
{
    public class AdOutboundController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        //
        // GET: /AdOutbound/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OutboundHtml(string shorturl)
        {
            string htmlRow = "Error when loading";
            try {
                htmlRow = context.SimpleAds.First(a => a.ShortUrlKey.Equals(shorturl)).HtmlSource;
            }
            catch (Exception ex)
            {}
            
            return PartialView("OutboundHtml",htmlRow);
        }
	}
}