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
        public ActionResult OutboundAbTest(string shorturl)
        {
            var test = service.GetAbTestByUrl(shorturl);
            int count = test.ChangeCount;
            int period = test.ChangeHours;
            var currentDate = DateTime.Now;
            int abCurrent = test.AdAId.Value;
            string abUrlCurrent = test.AdA.ShortUrlKey;
            if (currentDate.CompareTo(test.DateEnd) < 0 && currentDate.CompareTo(test.DateStart) > 0)
            {
                TimeSpan diff = currentDate - test.DateStart;
                double hours = diff.TotalHours;
                if ((hours / period + 1) % 2 == 0)
                {
                    abCurrent = test.AdBId.Value;
                    abUrlCurrent = test.AdB.ShortUrlKey;
                }
            }
            
            AbOutboundDTO model = new AbOutboundDTO
            {
                AdIdA = test.AdAId.GetValueOrDefault(0),
                AdIdB = test.AdBId.GetValueOrDefault(0),
                AbTestUrl = shorturl,
                AbTestId = test.Id,
                AdIdCurrent = abCurrent,
                AdUrlCurrent = abUrlCurrent
            };
            return PartialView("OutboundAbTest", model);
        }
	}
}