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
using ImpulseApp.Models.DTO;
using AutoMapper;
using ImpulseApp.MapUtils;
using System.Collections;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace ImpulseApp.Controllers.APIControllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    [RoutePrefix("")]
    public class AdController : ApiController
    {
        DBService.IDBService service = new DBService.DBServiceClient();
        [Route("api/ad/setactive/{adId}")]
        public async Task<HttpResponseMessage> SetActive(string adId)
        {
            int id = Convert.ToInt32(adId);
            await service.SetActiveByAdIdAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK);

        }
        [Route("api/ad/create")]
        [HttpPost]
        public HttpResponseMessage CreateAd(SimpleAdModelDTO modelDTO)
        {
            SimpleAdModel model = Mapper.Map<SimpleAdModelDTO, SimpleAdModel>(modelDTO);
            model.Init(User.Identity.GetUserId());
            service.SaveAd(model, true);
            String url = Url.Link("Default", new { controller = "UserFront", action = "CreateResponse", id = model.ShortUrlKey });
            var response = Request.CreateResponse(HttpStatusCode.Created, url);
            return response;
        }

        [Route("api/ad/remove")]
        [HttpPost]
        public HttpResponseMessage RemoveAdById(int id)
        {
            service.RemoveAdById(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [Route("api/ad/remove/url")]
        [HttpPost]
        public HttpResponseMessage RemoveAdByUrl(string url)
        {
            service.RemoveAdByUrl(url);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/ad")]
        public HttpResponseMessage GetAd(string url)
        {
            SimpleAdModel ad = service.GetAdByUrl(url);
            SimpleAdModelDTO adDto = Mapper.Map<SimpleAdModel, SimpleAdModelDTO>(ad);
            return Request.CreateResponse(HttpStatusCode.OK, adDto);

        }
        [Route("api/ad/{adId}")]
        public HttpResponseMessage GetAdById(int adId)
        {
            SimpleAdModel ad = service.GetAdById(adId);
            SimpleAdModelDTO adDto = Mapper.Map<SimpleAdModel, SimpleAdModelDTO>(ad);
            return Request.CreateResponse(HttpStatusCode.OK, adDto);

        }
        [Route("api/ad/all")]
        public HttpResponseMessage GetUserAds()
        {
            var userAds = service.GetUserAds(User.Identity.GetUserId());
            List<SimpleAdModelDTO> ads = new List<SimpleAdModelDTO>();
            foreach (var ad in userAds)
            {
                ads.Add(AdMapUtils.GetAdDTO(ad));
            }
            return Request.CreateResponse(HttpStatusCode.OK, ads);

        }
        [Route("api/ad/remove/{id}")]
        public HttpResponseMessage GetUserAds(int id)
        {
            var userAds = service.GetUserAds(User.Identity.GetUserId());
            List<SimpleAdModelDTO> ads = new List<SimpleAdModelDTO>();
            foreach (var ad in userAds)
            {
                ads.Add(AdMapUtils.GetAdDTO(ad));
            }
            return Request.CreateResponse(HttpStatusCode.OK, ads);

        }


    }
}
