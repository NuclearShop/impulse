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

namespace ImpulseApp.Controllers
{
    public class StatisticController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
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

            foreach (var activityJS in adJS["Activities"])
            {
                Activity activityDB = new Activity();

                activityDB.Session = adDB;
                activityDB.StartTime = DateTime.ParseExact((String)activityJS["StartTime"], "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                activityDB.EndTime = DateTime.ParseExact((String)activityJS["EndTime"], "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                foreach (var clickJS in activityJS["Clicks"])
                {
                    Click clickDB = new Click();
                    clickDB.Activity = activityDB;
                    clickDB.ClickTime = DateTime.ParseExact((String)clickJS["ClickTime"], "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    clickDB.ClickZone = (String)clickJS["ClickZone"];
                    clickDB.ClickType = Int16.Parse((String)clickJS["ClickType"]);
                    context.ClickStats.Add(clickDB);
                }
                context.Activity.Add(activityDB);
            }
            context.Sessions.Add(adDB);
            context.SaveChanges();

        }
        [HttpPost]
        public JsonResult ClickStatistics(List<int> AdIds, string sDate = "", string eDate = "",
            int interval = 1, bool isMinificate = false)
        {
            List<SimpleAdModel> ads = new List<SimpleAdModel>();
            
            foreach (int adId in AdIds)
            {
                var ad = context.SimpleAds.SingleOrDefault(a => a.Id == adId);
                ads.Add(ad);
            }
            CompareChartModel chart = new CompareChartModel();

            if (isMinificate && ads.Count==1)
            {
                SimpleAdModel ad = ads[0];
                StatChartJS model = new StatChartJS();
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
                }
                chart.charts.Add(model);
            }
            else
            {
                
                DateTime startDate = ads.Min(a=>a.AdSessions.OrderBy(b => b.DateTimeStart).First().DateTimeStart);
                DateTime endDate = ads.Max(a=>a.AdSessions.OrderByDescending(b => b.DateTimeStart).First().DateTimeStart);
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
                foreach(var ad in ads)
                {
                    StatChartJS model = new StatChartJS();
                    for (DateTime curDate = startDate; curDate < endDate; curDate = curDate.AddDays(interval))
                    {
                        DateTime estimate = curDate.AddDays(interval);
                        model.labels.Add(curDate.ToShortDateString());
                        var sessions = ad.AdSessions.Where(a => a.DateTimeStart.CompareTo(curDate) >= 0 && a.DateTimeStart.CompareTo(estimate) <= 0);
                        if(sessions.Count()==0)
                        {
                            model.data.Add("0");
                        }
                        else
                        {
                            model.data.Add(sessions.SelectMany(a => a.Activities).SelectMany(b => b.Clicks).Count().ToString());
                        }
                    }
                    chart.charts.Add(model);
                }

            }
            return Json(chart);
        }
    }
}