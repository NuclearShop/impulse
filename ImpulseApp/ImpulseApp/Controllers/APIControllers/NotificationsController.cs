using ImpulseApp.DBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ImpulseApp.Controllers.APIControllers
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        IDBService service = new DBServiceClient();
        [Route("api/notifications/reviews")]
        public async Task<HttpResponseMessage> GetReviewNotifications()
        {
            var views = await service.GetModeratorViewsAsync(User.Identity.GetUserId());
            return Request.CreateResponse(HttpStatusCode.OK, views);
        }
    }
}
