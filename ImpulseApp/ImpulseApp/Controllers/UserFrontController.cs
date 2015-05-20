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
using ImpulseApp.Models.Dicts;

namespace ImpulseApp.Controllers
{

    [Authorize]
    public class UserFrontController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        DBService.IDBService service = new DBService.DBServiceClient();
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
        public ActionResult CreateResponse(int id)
        {
            return View(id);
        }

        public ActionResult StatisticsIndex()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string id = User.Identity.GetUserId();
            var ads = service.GetUserAds(id);
            return View(ads);
        }
        public ActionResult StatisticsClicks()
        {
            return View();
        }
        public ActionResult StatisticsTableClick()
        {
            return View();
        }
        public ActionResult AdList()
        {
            return PartialView();
        }
        public ActionResult AbTest()
        {
            return PartialView();
        }


    }
}