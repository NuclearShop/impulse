using ImpulseApp.Models;
using ImpulseApp.Models.AdModels;
using ImpulseApp.Models.ComplexViewModels;
using ImpulseApp.Models.StatModels;
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
            SimpleAdModel model = null;
            try {
                model = context.SimpleAds.First(a => a.ShortUrlKey.Equals(shorturl));
                htmlRow = model.HtmlSource;
            }
            catch (Exception ex)
            {}

            AdOutboundModel aom = new AdOutboundModel
            {
                Session = new AdSession
                {
                    AdId = model.Id,
                },
                htmlString = htmlRow
            };
            return PartialView("OutboundHtml",aom);
        }
	}
}