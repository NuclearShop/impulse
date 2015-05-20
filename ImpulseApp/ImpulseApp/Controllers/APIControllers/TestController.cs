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
    public class TestController : ApiController
    {
        DBService.IDBService service = new DBService.DBServiceClient();
        [Route("api/ab/create")]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveAb(ABTest abtest)
        {
            await service.SaveAbTestAsync(abtest);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [Route("api/ab")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAb(string url)
        {
            var test = await service.GetAbTestByUrlAsync(url);
            test.AdA.ToDTO();
            test.AdB.ToDTO();
            return Request.CreateResponse(HttpStatusCode.OK, test);
        }
        [Route("api/ab/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAbById(int id)
        {
            var test = await service.GetAbTestByIdAsync(id);
            test.AdA.ToDTO();
            test.AdB.ToDTO();
            return Request.CreateResponse(HttpStatusCode.OK, test);
        }
        [Route("api/ab/all")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAbAll()
        {
            var tests = await service.GetAllActiveTestsAsync(User.Identity.GetUserId());
            foreach (var t in tests)
            {
                t.AdA.ToDTO();
                t.AdB.ToDTO();
            }

            return Request.CreateResponse(HttpStatusCode.OK, tests);
        }

        [Route("api/ab/review/{id}")]
        public async Task<HttpResponseMessage> GetAbReview(int id)
        {
            AbTestReviewModelDTO abReview = new AbTestReviewModelDTO();
            var test = await service.GetAbTestByIdAsync(id);
            abReview.AdA = Mapper.Map<SimpleAdModel, SimpleAdModelDTO>(test.AdA);
            abReview.AdB = Mapper.Map<SimpleAdModel, SimpleAdModelDTO>(test.AdB);

            abReview.OverallClicksA = test.AdA.AdSessions
                .Where(ab => ab.AbTestId.HasValue && ab.AbTestId == test.Id)
                .SelectMany(a => a.Activities)
                .SelectMany(b => b.Clicks)
                .Where(c => c.ClickTime.CompareTo(test.DateStart) >= 0 && c.ClickTime.CompareTo(test.DateEnd) <= 0)
                .Count();
            abReview.OverallClicksB = test.AdB.AdSessions
                .Where(ab => ab.AbTestId.HasValue && ab.AbTestId == test.Id)
                .SelectMany(a => a.Activities)
                .SelectMany(b => b.Clicks)
                .Where(c => c.ClickTime.CompareTo(test.DateStart) >= 0 && c.ClickTime.CompareTo(test.DateEnd) <= 0)
                .Count();
            abReview.AbTest = test;
            
            int i = 0;
            for (var dt = test.DateStart; dt.CompareTo(test.DateEnd) <= 0; dt = dt.AddHours(test.ChangeHours * 2))
            {
                DateTime de = dt.AddHours(test.ChangeHours * 2);
                AbCompareClickChart cc = new AbCompareClickChart();
                cc.AdAClicks = test.AdA.AdSessions
                    .Where(ab => ab.AbTestId.HasValue && ab.AbTestId == test.Id)
                    .SelectMany(a => a.Activities)
                    .SelectMany(b => b.Clicks)
                    .Where(c => c.ClickTime.CompareTo(dt) >= 0 && c.ClickTime.CompareTo(de) <= 0)
                    .Count();
                cc.AdBClicks = test.AdB.AdSessions
                    .Where(ab => ab.AbTestId.HasValue && ab.AbTestId == test.Id)
                    .SelectMany(a => a.Activities)
                    .SelectMany(b => b.Clicks)
                    .Where(c => c.ClickTime.CompareTo(dt) >= 0 && c.ClickTime.CompareTo(de) <= 0)
                    .Count();
                cc.Iteration = i++;
                abReview.ClickChart.Add(cc);
            }
            abReview.AbTest.AdA.ToDTO();
            abReview.AbTest.AdB.ToDTO();
            return Request.CreateResponse(HttpStatusCode.OK, abReview);
        }
    }
}