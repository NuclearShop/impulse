using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImpulseApp.Models;
using ImpulseApp.Models.StatModels;
using ImpulseApp.Models.ComplexViewModels;
using ImpulseApp.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ImpulseApp.Models.AdModels;
using ImpulseApp.Models.Dicts;
using Microsoft.AspNet.Identity;

namespace ImpulseApp.Controllers
{
    public class StatisticController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        DBService.IDBService service = new DBService.DBServiceClient();
        public ActionResult InitSession(AdOutboundModel model)
        {
            model.Session.DateTimeStart = DateTime.Now;
            model.Session.UserIp = Request.ServerVariables["REMOTE_ADDR"];
            model.Session.UserBrowser = Request.UserAgent;
            model.Session.UserLocale = Request.UserLanguages[0];
            return Json(model);
        }

        public ActionResult StartActivity(AdOutboundModel model)
        {
            Activity activity = new Activity
            {
                SessionId = model.Session.SessionId,
                StartTime = DateTime.Now,
                Session = model.Session
            };

            model.CreateCurrentActivity(activity);
            return Json(model);
        }

        public ActionResult StopActivity(AdOutboundModel model)
        {
            if (model.CurrentActivity != null)
            {
                model.EndActivity();
            }
            return Json(model);
        }

        [HttpPost]
        public JsonResult ClickStatistics(List<int> AdIds, string sDate = "", string eDate = "",
            int interval = 1, bool isMinificate = false)
        {
            List<SimpleAdModel> ads = new List<SimpleAdModel>();

            if (AdIds == null || AdIds.Count == 0)
            {
                return Json(new CompareChartModel());
            }
            foreach (int adId in AdIds)
            {
                var ad = service.GetAdById(adId);
                if (ad.AdSessions.Count > 0)
                    ads.Add(ad);
            }
            CompareChartModel chart = new CompareChartModel();
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            try
            {
                startDate = ads.Min(a => a.AdSessions.OrderBy(b => b.DateTimeStart).First().DateTimeStart);
                endDate = ads.Max(a => a.AdSessions.OrderByDescending(b => b.DateTimeStart).First().DateTimeStart);
            }
            catch (Exception ex)
            {
                return Json(chart);
            }
            if (sDate != "")
            {
                DateTime dt = DateTime.Parse(sDate);
                if (dt.CompareTo(startDate) > 0)
                {
                    startDate = dt;
                }
            }
            if (eDate != "")
            {
                DateTime dt = DateTime.Parse(eDate);
                if (dt.CompareTo(endDate) < 0)
                {
                    endDate = dt;
                }
            }
            foreach (var ad in ads)
            {
                StatChartJS model = new StatChartJS();
                model.name = ad.Name;
                for (DateTime curDate = startDate; curDate <= endDate; curDate = curDate.Date.AddDays(interval))
                {
                    DateTime estimate = curDate.Date.AddDays(interval);
                    model.labels.Add(curDate.ToShortDateString());
                    List<AdSession> sessions = new List<AdSession>();
                    foreach (var s in ad.AdSessions)
                    {
                        if (s.DateTimeStart.Date.CompareTo(curDate.Date) >= 0 && s.DateTimeStart.CompareTo(estimate.Date) <= 0)
                        {
                            sessions.Add(s);
                        }
                    }
                    if (sessions.Count() == 0)
                    {
                        model.data.Add("0");
                    }
                    else
                    {
                        model.data.Add(sessions.SelectMany(a => a.Activities).SelectMany(b => b.Clicks).Count().ToString());
                    }
                }
                model.name = ad.Name;
                chart.charts.Add(model);

            }

            return Json(chart);
        }

        public PartialViewResult DetailedStatistics(DateTime Date, List<int> AdIds)
        {
            //var sessionsTemp = context.SimpleAds.Where(a => AdIds.Contains(a.Id)).SelectMany(b => b.AdSessions);

            List<AdSession> sessions = new List<AdSession>();
            foreach (int id in AdIds)
            {
                sessions.AddRange(service.GetAdById(id).AdSessions);
            }
            List<AdSession> sessionsFiltered = new List<AdSession>();
            foreach (var session in sessions)
            {
                if (session.DateTimeStart.Date.Equals(Date.Date))
                {
                    sessionsFiltered.Add(session);
                }

            }
            return PartialView(sessionsFiltered);
        }

        public JsonResult BrowserStatistics(List<int> AdIds, string sDate = "", string eDate = "")
        {
            List<SimpleAdModel> ads = new List<SimpleAdModel>();
            CompareChartModel chart = new CompareChartModel();
            if (AdIds == null || AdIds.Count == 0)
            {
                return Json(new StatChartJS());
            }
            foreach (int adId in AdIds)
            {
                var ad = service.GetAdById(adId);
                ads.Add(ad);
            }
            DateTime startDateTime = DateTime.MinValue;
            DateTime endDateTime = DateTime.MaxValue;
            if (sDate != "")
            {
                startDateTime = DateTime.Parse(sDate);
            }
            if (eDate != "")
            {
                endDateTime = DateTime.Parse(eDate);
            }
            foreach (var ad in ads)
            {
                StatChartJS jsModel = new StatChartJS();
                jsModel.labels = Browsers.NAMES.ToList();
                jsModel.name = ad.Name;
                foreach (var browserName in jsModel.labels)
                {
                    jsModel.data.Add(ad.AdSessions
                        .Where(b => b.UserBrowser.Equals(browserName)
                        && b.DateTimeStart.CompareTo(startDateTime) >= 0
                        && b.DateTimeEnd.CompareTo(endDateTime) <= 0).Count().ToString());
                }
                chart.charts.Add(jsModel);
            }
            return Json(chart);
        }

        public JsonResult LocaleStatistics(List<int> AdIds, string sDate = "", string eDate = "")
        {
            var ads = service.GetUserAds(User.Identity.GetUserId()).Where(a=>AdIds.Contains(a.Id));

            List<LocaleVisitorsTable> table = new List<LocaleVisitorsTable>();
            CompareChartModel chartCompare = new CompareChartModel();
            //var sessionsByWeek = ads.SelectMany(a => a.AdSessions).Where(a => a.DateTimeStart.Date.CompareTo(date.Date) == 0);
            var sessionsByLocale = ads.SelectMany(a => a.AdSessions).GroupBy(a => a.UserLocale);
            StatChartJS chartModel = new StatChartJS();
            chartModel.name = "Лояльность по странам";
            foreach (var session in sessionsByLocale)
            {

                LocaleVisitorsTable row = new LocaleVisitorsTable();
                row.Locale = session.Key;
                row.ViewsByWeek = session.Count(a => a.DateTimeStart.AddDays(7).CompareTo(DateTime.Now) > 0).ToString();
                row.ViewsByMonth = session.Count(a => a.DateTimeStart.AddMonths(1).CompareTo(DateTime.Now) > 0).ToString();
                chartModel.labels.Add(session.Key);
                chartModel.data.Add(row.ViewsByMonth);

                row.PopularPresentation = ads.First(b => b.Id == session
                    .GroupBy(a => a.AdId)
                    .OrderByDescending(a => a.Count())
                    .First().Key).Name;
                table.Add(row);
            }
            chartCompare.charts.Add(chartModel);
            return Json(chartCompare);
        }

        public JsonResult FunnelStatistics(List<int> AdIds, string sDate = "", string eDate = "")
        {
            List<SimpleAdModel> ads = new List<SimpleAdModel>();
            CompareChartModel chart = new CompareChartModel();
            if (AdIds == null || AdIds.Count == 0)
            {
                return Json(new StatChartJS());
            }
            foreach (int adId in AdIds)
            {
                var ad = service.GetAdById(adId);
                ads.Add(ad);
            }
            DateTime startDateTime = DateTime.MinValue;
            DateTime endDateTime = DateTime.MaxValue;
            if (sDate != "")
            {
                startDateTime = DateTime.Parse(sDate);
            }
            if (eDate != "")
            {
                endDateTime = DateTime.Parse(eDate);
            }
            foreach (var ad in ads)
            {
                StatChartJS jsModel = new StatChartJS();
                var clicks = ad.AdSessions
                        .SelectMany(a => a.Activities)
                        .Where(c => c.StartTime.CompareTo(startDateTime) >= 0
                            && c.EndTime.CompareTo(endDateTime) <= 0).GroupBy(d => d.CurrentStateName).OrderByDescending(e => e.Count());
                jsModel.name = ad.Name;
                foreach (var clickGroup in clicks)
                {
                    var clickElem = clickGroup.First();
                    //AdState state = service.GetStateByAdIdAndVideoId(ad.Id, clickElem.ClickCurrentStage, clickElem.Activity.CurrentStateName);
                    jsModel.labels.Add(clickElem.CurrentStateName);
                    jsModel.data.Add(clickGroup.Count().ToString());
                }
                chart.charts.Add(jsModel);
            }
            return Json(chart);
        }
        [HttpPost]
        public ActionResult TableClickStatistics(int AdId, String StartDate = "", String EndDate = "")
        {
            SimpleAdModel ad = service.GetAdById(AdId);

            List<TableRowModel> table = new List<TableRowModel>();
            DateTime StartDateTime = DateTime.MinValue;
            DateTime EndDateTime = DateTime.MaxValue;
            if (StartDate != "")
            {
                StartDateTime = DateTime.Parse(StartDate);
            }
            if (EndDate != "")
            {
                EndDateTime = DateTime.Parse(EndDate);
            }
            var sessionsByIP = ad.AdSessions.Where(b => b.DateTimeStart.CompareTo(StartDateTime) > 0 && b.DateTimeEnd.CompareTo(EndDateTime) < 0).GroupBy(a => a.UserIp);
            foreach (var session in sessionsByIP)
            {
                TableRowModel row = new TableRowModel();
                row.IP = session.Key;
                row.Locale = session.First().UserLocale;
                row.Browser = session.First().UserBrowser;
                var clicks = session
                        .SelectMany(a => a.Activities)
                        .SelectMany(b => b.Clicks).GroupBy(d => d.ClickStamp).OrderByDescending(e => e.Count());
                string transitions = ":";
                foreach (var clickGroup in clicks)
                {
                    transitions += (":" + clickGroup.First().ClickStamp);
                }
                row.Stages.Add(new Stage
                {
                    Name = "Переходы",
                    Value = transitions
                });
                //row.FirstStage = session.SelectMany(a => a.Activities).SelectMany(b => b.Clicks).Where(c => c.ClickType == 0).Count().ToString();
                //row.SecondStage = session.SelectMany(a => a.Activities).SelectMany(b => b.Clicks).Where(c => c.ClickType == 1).Count().ToString();
                //row.ThirdStage = session.SelectMany(a => a.Activities).SelectMany(b => b.Clicks).Where(c => c.ClickType == 2).Count().ToString();
                table.Add(row);
            }
            return PartialView(table);
        }

        [HttpPost]
        public ActionResult UniqueVisitorsStatistics(String StartDate = "", String EndDate = "", int daysStep = 1)
        {
            var ads = service.GetUserAds(User.Identity.GetUserId());

            List<UniqueVisitorsTable> table = new List<UniqueVisitorsTable>();
            DateTime StartDateTime = ads.Select(a=>a.DateTime).Min();
            DateTime EndDateTime = ads.Select(a=>a.DateTime).Max();
            if (StartDate != "")
            {
                var incomeTime = DateTime.Parse(StartDate);
                if(incomeTime.CompareTo(StartDateTime)>0) {
                    StartDateTime = incomeTime;
                }
                
            }
            if (EndDate != "")
            {
                var incomeTime = DateTime.Parse(EndDate);
                if(incomeTime.CompareTo(EndDateTime)<0) {
                    EndDateTime = incomeTime;
                }
            }
            
            for (var date = StartDateTime; date < EndDateTime; date = date.AddDays(daysStep))
            {
                UniqueVisitorsTable row = new UniqueVisitorsTable();
                var sessions = ads.SelectMany(a => a.AdSessions).Where(a=>a.DateTimeStart.Date.CompareTo(date.Date)==0);
                var sessionsByIP = sessions.Select(a=>a.UserIp).Distinct();
                row.Date = date.ToShortDateString();
                row.UniqueVisitors = sessionsByIP.Count();
                row.AverageTime = sessions.Average(a => a.DateTimeEnd.Ticks - a.DateTimeStart.Ticks).ToString();
                row.MaxTime = sessions.Max(a => a.DateTimeEnd.Ticks - a.DateTimeStart.Ticks).ToString();
                table.Add(row);
            }
            return PartialView(table);
        }
    }
}