using ImpulseApp.Filters;
using ImpulseApp.Models;
using ImpulseApp.Models.AdModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using ImpulseApp.Models.StatModels;

namespace ImpulseApp.Controllers
{

    [Authorize]
    public class UserFrontController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        //
        // GET: /UserFront/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [Route("UserFront/Create")]
        public ActionResult CreateTestStub()
        {
            return View();
        }
        [RestApiAttribute]
        [HttpPost]
        public JsonResult CreateExecuting(string xmlMessage)
        {
            XDocument xmlModel = XDocument.Parse(xmlMessage);
            SimpleAdModel mvcAdModel = new SimpleAdModel();
            var elems = xmlModel.Root.Elements();
            var body = elems.Single(el => el.Name.LocalName.Equals("body"));
            mvcAdModel.HtmlSource = body.Value.Trim();
            context.SimpleAds.Add(mvcAdModel);
            context.SaveChangesAsync();
            return Json(new { redirectToUrl = Url.Action("CreateResponse", new { id = mvcAdModel.ShortUrlKey }) });
        }
        public ActionResult CreateResponse(int id)
        {
            return View(id);
        }

        public ActionResult StatisticsIndex()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string id = User.Identity.GetUserId();
            var ads = context.SimpleAds.Where(a => a.UserId.Equals(id));
            return View(ads);
        }
        public ActionResult StatisticsClicks()
        {
            return View();
        }


    }
}