using ImpulseApp.DBService;
using ImpulseApp.Models.AdModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImpulseApp.Controllers.APIControllers
{
    public class VideoController : ApiController
    {
        IDBService service = new DBServiceClient();

        [Route("api/video/all")]
        public IEnumerable<VideoUnit> GetUserVideos()
        {
            IEnumerable<VideoUnit> videos = service.GetUserVideo(User.Identity.Name);
            return videos;
        }
    }
}
