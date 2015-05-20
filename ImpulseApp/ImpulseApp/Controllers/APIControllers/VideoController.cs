using ImpulseApp.DBService;
using ImpulseApp.Models.AdModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web.Http.Cors;

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
        [Route("api/video/ids")]
        public IEnumerable<int> GetVideoIds()
        {
            var ids = service.GetUserVideo(User.Identity.GetUserId()).Select(a => a.Id);
            return ids;
        }
        [Route("api/video/{id}")]
        public VideoUnit GetVideoUnitById(int id)
        {
            var video = service.GetVideoById(User.Identity.GetUserId(), id);
            return video;
        }

    }
}
