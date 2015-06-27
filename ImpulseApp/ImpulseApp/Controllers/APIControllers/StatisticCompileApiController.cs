using ImpulseApp.Models.StatModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Globalization;

namespace ImpulseApp.Controllers.APIControllers
{
    [Authorize]
    public class StatisticCompileApiController : ApiController
    {

        DBService.IDBService service = new DBService.DBServiceClient();
        [Route("api/reports/visitors")]
        [HttpGet]
        public HttpResponseMessage UniqueVisitorsStatistics(String StartDate = "", String EndDate = "", int daysStep = 1)
        {
            var ads = service.GetUserAds(User.Identity.GetUserId());
            List<UniqueVisitorsTable> table = new List<UniqueVisitorsTable>();
            if (ads.Count() > 0)
            {


                DateTime StartDateTime = ads.SelectMany(a => a.AdSessions).Select(a => a.DateTimeStart).Min().Date;
                DateTime EndDateTime = DateTime.Now.Date;
                if (!String.IsNullOrEmpty(StartDate))
                {
                    DateTime incomeTime = DateTime.MaxValue;
                    DateTime.TryParse(StartDate, new CultureInfo("ru-RU"), DateTimeStyles.None, out incomeTime);
                    if (incomeTime.CompareTo(StartDateTime) > 0)
                    {
                        StartDateTime = incomeTime;
                    }

                }
                if (!String.IsNullOrEmpty(EndDate))
                {
                    DateTime incomeTime = DateTime.MaxValue;
                    DateTime.TryParse(EndDate, new CultureInfo("ru-RU"), DateTimeStyles.None, out incomeTime);
                    if (incomeTime.CompareTo(EndDateTime) < 0 && incomeTime.CompareTo(DateTime.MinValue) > 0)
                    {
                        EndDateTime = incomeTime;
                    }
                }

                for (var date = StartDateTime; date.CompareTo(EndDateTime) < 0; date = date.AddDays(daysStep))
                {
                    UniqueVisitorsTable row = new UniqueVisitorsTable();
                    var sessions = ads.SelectMany(a => a.AdSessions).Where(a => a.DateTimeStart.Date.CompareTo(date.Date) == 0);
                    if (daysStep > 1)
                    {
                        sessions = ads.SelectMany(a => a.AdSessions).Where(a => a.DateTimeStart.Date.CompareTo(date.Date) > 0 &&
                            a.DateTimeEnd.Date.CompareTo(date.AddDays(daysStep)) < 0);
                    }
                    var sessionsByIP = sessions.Select(a => a.UserIp).Distinct();
                    if (sessions.Count() > 0)
                    {
                        row.Date = date.ToShortDateString();
                        if (daysStep > 1)
                        {
                            row.Date = "От " + date.ToShortDateString() + " До " + date.AddDays(daysStep).ToShortDateString();
                        }

                        row.UniqueVisitors = sessionsByIP.Count();
                        row.AverageTime = Math.Floor(TimeSpan.FromTicks(
                            Convert.ToInt64(sessions.Average(a => a.DateTimeEnd.Ticks - a.DateTimeStart.Ticks))).TotalSeconds).ToString();
                        row.MaxTime = Math.Floor(TimeSpan.FromTicks(
                            Convert.ToInt64(sessions.Max(a => a.DateTimeEnd.Ticks - a.DateTimeStart.Ticks))).TotalSeconds).ToString();
                        table.Add(row);
                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/reports/locale")]
        [HttpGet]
        public HttpResponseMessage LocaleVisitorsStatistics(bool chart = false)
        {
            var ads = service.GetUserAds(User.Identity.GetUserId());
            List<LocaleVisitorsTable> table = new List<LocaleVisitorsTable>();
            CompareChartModel chartCompare = new CompareChartModel();
            if (ads.Count > 0)
            {
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
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/reports/chart/locale")]
        [HttpPost]
        public HttpResponseMessage LocaleVisitorsStatisticsChart()
        {
            var ads = service.GetUserAds(User.Identity.GetUserId());
            List<LocaleVisitorsTable> table = new List<LocaleVisitorsTable>();
            CompareChartModel chartCompare = new CompareChartModel();
            if (ads.Count() > 0)
            {
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
            }
            return Request.CreateResponse(HttpStatusCode.OK, chartCompare);
        }
    }


}
