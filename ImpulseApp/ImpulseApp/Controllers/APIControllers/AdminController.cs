using ImpulseApp.DBService;
using ImpulseApp.MapUtils;
using ImpulseApp.Models.DTO;
using ImpulseApp.Models.UtilModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImpulseApp.Controllers.APIControllers
{
    [Authorize(Users = "main,main@impulse.ru")]
    public class AdminController : ApiController
    {
        IDBService service = new DBServiceClient();

        [Route("api/admin/{skip}/{take}")]
        public HttpResponseMessage GetLastVideos(int skip, int take)
        {
            var videos = service.GetLastAds(skip, take);
            var reviews = service.GetModeratorViews("admin@admin");
            List<SimpleAdModelDTO> ads = new List<SimpleAdModelDTO>();
            List<ModeratorViewDTO> views = new List<ModeratorViewDTO>();
            foreach (var ad in videos)
            {
                var adDto = AdMapUtils.GetAdDTO(ad);
                ModeratorViewDTO viewDto = new ModeratorViewDTO
                {
                    ad = adDto,
                    review = new ModeratorView
                    {
                        AdId = ad.Id
                    }
                };
                var review = reviews.FirstOrDefault(a => a.AdId == ad.Id);
                if (review != null)
                {
                    viewDto.review = review;
                }
                views.Add(viewDto);
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, views);
        }
        [Route("api/admin/reviews")]
        public HttpResponseMessage GetViews(int skip, int take)
        {
            var reviewed = service.GetModeratorViews("admin@admin");
            return Request.CreateResponse(HttpStatusCode.OK, reviewed);
        }
        [Route("api/admin/view")]
        [HttpPost]
        public HttpResponseMessage SaveReview(ModeratorView view)
        {
            view.ViewDateTime = DateTime.Now;
            string id = service.SaveModeratorView(view);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }
    }
}
