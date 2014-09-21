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
        [Route("/UserFront/Create")]
        public ActionResult CreateTestStub()
        {
            return View();
        }
        [RestApiAttribute]
        [HttpPost]
        public ActionResult CreateExecuting(string xmlMessage)
        {


            XDocument xmlModel = XDocument.Parse(xmlMessage);

            SimpleAdModel mvcAdModel = new SimpleAdModel();

            mvcAdModel.HtmlSource = xmlModel.DescendantNodes().Single(el => el.NodeType == XmlNodeType.CDATA).Parent.Value.Trim();

            context.SimpleAds.Add(mvcAdModel);

            context.SaveChangesAsync();

            return RedirectToAction("CreateResponse", new { id = mvcAdModel.Id});
        }
        public ActionResult CreateResponse()
        {
            return View();
        }
	}
}