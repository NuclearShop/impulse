using ImpulseApp.Models.Dicts;
using ImpulseApp.Models.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ImpulseApp.Database
{
    public class DBService : IDBService
    {

        ImpulseContext context = new ImpulseContext();
        public DBService()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        public IEnumerable<Models.AdModels.SimpleAdModel> GetUserAds(string UserId)
        {
            return context.SimpleAds.Where(a => a.UserId.Equals(UserId)).ToArray();
        }

        public string SaveAd(Models.AdModels.SimpleAdModel model, bool proceedToDB = true)
        {
            context.SimpleAds.Add(model);
            try
            {
                if (proceedToDB)
                    context.SaveChanges();
                return ResponseStatuses.SUCCESS;
            }
            catch (Exception ex)
            {
                return ResponseStatuses.BuildErrorResponse(ex.InnerException.Message);
            }
        }

        public IEnumerable<Models.StatModels.AdSession> GetSessionsByAdId(int AdId)
        {
            return context.Sessions.Where(a => a.AdId == AdId).ToList();
        }

        public string SaveAdSession(Models.StatModels.AdSession model, bool proceedToDB = true)
        {
            context.Sessions.Add(model);
            try
            {
                if (proceedToDB)
                    context.SaveChanges();
                return ResponseStatuses.SUCCESS;
            }
            catch (Exception ex)
            {
                return ResponseStatuses.BuildErrorResponse(ex.InnerException.Message);
            }
        }

        public IEnumerable<Models.StatModels.Activity> GetActivityBySessionId(int SessionId)
        {
            return context.Activity.Where(a => a.SessionId == SessionId);
        }

        public string SaveActivity(Models.StatModels.Activity model, bool proceedToDB = true)
        {
            context.Activity.Add(model);
            try
            {
                if (proceedToDB)
                    context.SaveChanges();
                return ResponseStatuses.SUCCESS;
            }
            catch (Exception ex)
            {
                return ResponseStatuses.BuildErrorResponse(ex.InnerException.Message);
            }
        }

        public IEnumerable<Models.StatModels.Click> GetClicksByActivityId(int ActivityId)
        {
            return context.ClickStats.Where(a => a.ActivityId == ActivityId).ToList();
        }

        public string SaveClick(Models.StatModels.Click model, bool proceedToDB = true)
        {
            context.ClickStats.Add(model);
            try
            {
                if (proceedToDB)
                    context.SaveChanges();
                return ResponseStatuses.SUCCESS;
            }
            catch (Exception ex)
            {
                return ResponseStatuses.BuildErrorResponse(ex.InnerException.Message);
            }
        }


        public Models.AdModels.SimpleAdModel GetAdByUrl(string url)
        {
            var ad = context.SimpleAds.First(a => a.ShortUrlKey.Equals(url));
            return ad;
        }

        [ReferencePreservingDataContractFormatAttribute]
        public Models.AdModels.SimpleAdModel GetAdById(int id)
        {
            Models.AdModels.SimpleAdModel model = context.SimpleAds.Include("AdSessions").Include("AdSessions.Activities").Include("AdSessions.Activities.Clicks").Include("AdStates").SingleOrDefault(a => a.Id == id);
            
            return model;
        }



        public string SaveVideo(Models.AdModels.VideoUnit model)
        {
            context.VideoUnits.Add(model);
            context.SaveChanges();
            return ResponseStatuses.SUCCESS;
        }


        public IEnumerable<Models.AdModels.VideoUnit> GetUserVideo(string UserName)
        {
            return context.VideoUnits.Where(a => a.UserName.Equals(UserName)).ToList();
        }
    }
}
