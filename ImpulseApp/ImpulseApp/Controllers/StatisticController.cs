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
        [RestApiAttribute]
        [HttpPost]
        public void SaveSession(string xmlMessage)
        {

            var adJS = (JObject)JsonConvert.DeserializeObject(xmlMessage);
            AdSession adDB = new AdSession();
            String dtStartString = (String)adJS["AdTimeStart"];
            String dtEndString = (String)adJS["AdTimeEnd"];
            adDB.ActiveMilliseconds = 0;//DateTime.Parse((String)adJS["AdTimeStart"]).Subtract(DateTime.Parse((String)adJS["AdTimeEnd"])).Milliseconds;
            adDB.AdId = Int16.Parse((String)adJS["AdId"]);
            adDB.DateTimeStart = DateTime.ParseExact(dtStartString, "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            adDB.DateTimeEnd = DateTime.ParseExact(dtEndString, "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            adDB.UserIp = Request.ServerVariables["REMOTE_ADDR"];
            adDB.UserBrowser = Request.UserAgent;
            adDB.UserLocale = Request.UserLanguages[0];
            adDB.UserLocation = "123";
            List<Click> clicks = new List<Click>();
            List<Activity> activities = new List<Activity>();
            foreach (var activityJS in adJS["Activities"])
            {
                Activity activityDB = new Activity();

                //activityDB.Session = adDB;
                activityDB.StartTime = DateTime.ParseExact((String)activityJS["StartTime"], "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                activityDB.EndTime = DateTime.ParseExact((String)activityJS["EndTime"], "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                foreach (var clickJS in activityJS["Clicks"])
                {
                    Click clickDB = new Click();
                    //clickDB.Activity = activityDB;
                    clickDB.ClickTime = DateTime.ParseExact((String)clickJS["ClickTime"], "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    clickDB.ClickZone = (String)clickJS["ClickZone"];
                    clickDB.ClickType = Int16.Parse((String)clickJS["ClickType"]);
                    activityDB.Clicks.Add(clickDB);
                }
                adDB.Activities.Add(activityDB);
            }
            service.SaveAdSession(adDB, true);

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

            if (isMinificate && ads.Count == 1)
            {
                SimpleAdModel ad = ads[0];
                StatChartJS model = new StatChartJS();
                model.name = ad.Name;
                foreach (var session in ad.AdSessions.GroupBy(a => a.DateTimeStart.Date))
                {
                    model.labels.Add(session.Key.ToShortDateString());
                    int clickNum = 0;
                    foreach (var activityWrapper in session.Select(a => a.Activities))
                    {
                        foreach (var activity in activityWrapper)
                        {
                            clickNum += activity.Clicks.Count;
                        }
                    }
                    model.data.Add(clickNum.ToString());
                    model.name = ad.Name;
                }
                chart.charts.Add(model);
            }
            else if (ads.Count > 0)
            {

                DateTime startDate = ads.Min(a => a.AdSessions.OrderBy(b => b.DateTimeStart).First().DateTimeStart);
                DateTime endDate = ads.Max(a => a.AdSessions.OrderByDescending(b => b.DateTimeStart).First().DateTimeStart);
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
                        //var sessions = ad.AdSessions.Where(a => a.DateTimeStart.CompareTo(curDate) >= 0 && a.DateTimeStart.CompareTo(estimate) < 0);
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

        public JsonResult BrowserStatistics(List<int> AdIds)
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
            foreach (var ad in ads)
            {
                StatChartJS jsModel = new StatChartJS();
                jsModel.labels = Browsers.NAMES.ToList();
                jsModel.name = ad.Name;
                foreach (var browserName in jsModel.labels)
                {
                    jsModel.data.Add(ad.AdSessions.Where(b => b.UserBrowser.Equals(browserName)).Count().ToString());
                }
                chart.charts.Add(jsModel);
            }
            return Json(chart);
        }

        public JsonResult FunnelStatistics(List<int> AdIds)
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
            foreach (var ad in ads)
            {
                StatChartJS jsModel = new StatChartJS();
                jsModel.labels.Add("Просмотр 1 этапа");
                jsModel.labels.Add("Просмотр 2 этапа");
                jsModel.labels.Add("Просмотр 3 этапа");
                jsModel.name = ad.Name;
                for (int i = 0; i < 3; i++)
                {
                    jsModel.data.Add(ad.AdSessions.SelectMany(a => a.Activities).SelectMany(b => b.Clicks).Where(c => c.ClickType == i).Count().ToString());
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
                row.FirstStage = session.SelectMany(a => a.Activities).SelectMany(b => b.Clicks).Where(c => c.ClickType == 0).Count().ToString();
                row.SecondStage = session.SelectMany(a => a.Activities).SelectMany(b => b.Clicks).Where(c => c.ClickType == 1).Count().ToString();
                row.ThirdStage = session.SelectMany(a => a.Activities).SelectMany(b => b.Clicks).Where(c => c.ClickType == 2).Count().ToString();
                table.Add(row);
            }
            return PartialView(table);
        }
    }
}