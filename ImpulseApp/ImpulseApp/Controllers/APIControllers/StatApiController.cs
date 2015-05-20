using ImpulseApp.Models.StatModels;
using ImpulseApp.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ImpulseApp.Controllers.APIControllers
{
    public class StatApiController : ApiController
    {
        DBService.IDBService service = new DBService.DBServiceClient();
        [Route("api/stat/session")]
        public HttpResponseMessage SaveSession(AdSession Session)
        {
            if (Session.AdId != 0)
            {
                var httpContext = (HttpContextWrapper)Request.Properties["MS_HttpContext"];
                Session.UserIp = httpContext.Request.UserHostAddress;
                Session.UserLocale = httpContext.Request.UserLanguages[0];
                Session.UserLocation = "123";
                foreach (var activity in Session.Activities)
                {
                    foreach (var click in activity.Clicks)
                    {
                        click.ClickText = Generator.GenerateClickName(click);
                        click.ClickStamp = Generator.GenerateClickStamp(click);
                    }
                }
                service.SaveAdSession(Session, true);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}
