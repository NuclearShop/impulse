using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImpulseApp.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
        public ActionResult UploadTest()
        {
            return View();
        }
        public ActionResult GetAllVideosTest()
        {
            return View();
        }
	}
}