using ImpulseApp.Models;
using ImpulseApp.Models.AdModels;
using ImpulseApp.Models.ComplexViewModels;
using ImpulseApp.Models.DTO;
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
        DBService.IDBService service = new DBService.DBServiceClient();
        //
        // GET: /AdOutbound/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OutboundHtml(string shorturl)
        {
            int id = service.GetAdByUrl(shorturl).Id;
            AdOutboundDTO model = new AdOutboundDTO
            {
                AdId = id,
                AdUrl = shorturl
            };
            return PartialView("OutboundHtml", model);
        }
	}
}