using ImpulseApp.Models.AdModels;
using ImpulseApp.Models.Dicts;
using ImpulseApp.Models.Exception;
using ImpulseApp.Models.Exceptions;
using ImpulseApp.Models.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RefactorThis.GraphDiff;
using ImpulseApp.Models.DTO;

namespace ImpulseApp.Database
{
    public class DBService : IDBService
    {

        ImpulseContext context = new ImpulseContext();
        public DBService()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [ReferencePreservingDataContractFormat]
        public IEnumerable<Models.AdModels.SimpleAdModel> GetUserAds(string UserId)
        {
            return context.SimpleAds.Include("AdStates").Include("AdStates.VideoUnit").Where(a => a.UserId.Equals(UserId)).ToArray();
        }

        public string SaveAd(Models.AdModels.SimpleAdModel model, bool proceedToDB = true)
        {
            if (model.ShortUrlKey != null && model.Id > 0)
            {
                context.UpdateGraph(model, map => map
                    .OwnedCollection(graph => graph.StateGraph)
                    .OwnedCollection(states => states.AdStates, with => with
                        .AssociatedCollection(c => c.UserElements)
                        .AssociatedEntity(d => d.VideoUnit)));
            }
            else
            {
                context.SimpleAds.Add(model);
            }
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

        [ReferencePreservingDataContractFormatAttribute]
        public Models.AdModels.SimpleAdModel GetAdByUrl(string url)
        {
            var ad = context.SimpleAds
                .Include("StateGraph")
                .Include("AdStates")
                .Include("AdSessions")
                .Include("AdStates.VideoUnit")
                .Include("AdStates.UserElements")
                .Include("AdStates.UserElements.HtmlTags")
                .First(a => a.ShortUrlKey.Equals(url) && a.IsActive);
            return ad;
        }

        [ReferencePreservingDataContractFormatAttribute]
        public Models.AdModels.SimpleAdModel GetAdById(int id)
        {
            var ad = context.SimpleAds
                .Include("StateGraph")
                .Include("AdStates")
                .Include("AdSessions")
                .Include("AdSessions.Activities")
                .Include("AdSessions.Activities.Clicks")
                .Include("AdStates.VideoUnit")
                .Include("AdStates.UserElements")
                .Include("AdStates.UserElements.HtmlTags")
                .FirstOrDefault(a => a.Id == id);

            return ad;
        }



        public string SaveVideo(Models.AdModels.VideoUnit model)
        {
            context.VideoUnits.Add(model);
            context.SaveChanges();
            context.Entry<VideoUnit>(model).GetDatabaseValues();
            return model.Id.ToString();
        }


        public IEnumerable<Models.AdModels.VideoUnit> GetUserVideo(string UserName)
        {
            return context.VideoUnits.Where(a => a.UserName.Equals(UserName)).ToList();
        }


        public Models.AdModels.VideoUnit GetVideoById(string UserName, int Id)
        {
            VideoUnit videoUnit = context.VideoUnits.Find(Id);
            if (videoUnit == null)
            {
                throw new FaultException<VideoNotFoundException>(new VideoNotFoundException(Models.Properties.ExceptionText.VideoNotFoundById));
            }
            if (true)
            {
                return videoUnit;
            }
            else
            {
                throw new FaultException<VideoNotFoundException>(new VideoNotFoundException(Models.Properties.ExceptionText.VideoNotFoundByUser));
            }
        }





        public AdState GetStateByAdIdAndVideoId(int adId, int videoId, string stateName)
        {
            AdState state = null;
            try
            {
                state = context.AdStates.SingleOrDefault(a => a.AdId == adId && a.VideoUnitId == videoId && a.Name.Equals(stateName));
            }
            catch (Exception ex)
            {
                throw new FaultException<MultiplyAdStatesFoundException>(new MultiplyAdStatesFoundException(Models.Properties.ExceptionText.AdStateMultiply));
            }
            if (state == null)
            {
                throw new FaultException<AdStateNotFoundException>(new AdStateNotFoundException(Models.Properties.ExceptionText.AdStateNotFound));
            }
            return state;
        }


        public string SaveAdVersioningByEntity(Versioning versioning)
        {
            context.Entry<Versioning>(versioning).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return ResponseStatuses.SUCCESS;
        }

        public string SaveAdVersioningByIds(int rootId, int childId)
        {
            Versioning versioning = null;
            try
            {
                versioning = context.Versioning.Single(a => a.RootAdId == rootId && a.ChildAdId == childId);
            }
            catch (Exception ex)
            { }
            if (versioning == null)
            {
                versioning = new Versioning();
                versioning.RootAdId = rootId;
                versioning.ChildAdId = childId;
                context.Versioning.Add(versioning);
                context.SaveChanges();
                return ResponseStatuses.SUCCESS;
            }
            else
            {
                throw new FaultException<VersioningException>(new VersioningException("Текущая презентация является версией более чем одной презентации"));
            }

        }

        public bool IsRoot(int adId)
        {
            int count = context.Versioning.Where(a => a.RootAdId == adId).Count();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public int GetRootAdId(int adId)
        {
            try
            {
                var versioning = context.Versioning.Single(a => a.ChildAdId == adId);
                if (versioning != null)
                {
                    return versioning.RootAdId;
                }
                else
                {
                    if (context.Versioning.Where(a => a.RootAdId == adId).Count() > 0)
                    {
                        throw new FaultException<VersioningException>(new VersioningException("Текущая версия является корневой"));
                    }
                    throw new FaultException<VersioningException>(new VersioningException("Текущая презентация не найдена"));
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<VersioningException>(new VersioningException("Текущая презентация является версией более чем одной презентации"));
            }

        }

        public IEnumerable<Models.DTO.VersioningDTO> GetChildAds(int adId)
        {
            List<VersioningDTO> result = new List<VersioningDTO>();
            var versions = context.Versioning.Where(a => a.RootAdId == adId);
            if (versions == null)
            {
                throw new FaultException<VersioningException>(new VersioningException("Результаты не найдены"));
            }
            foreach (var v in versions)
            {
                result.Add(new VersioningDTO
                    {
                        ChildAdId = v.ChildAdId,
                        RootAdId = v.RootAdId,
                        date = v.Date
                    });
            }
            return result;
        }


        public string SaveAdVersioningByIdAndUrl(int rootId, string childUrl)
        {
            int childId = context.SimpleAds.FirstOrDefault(a => a.ShortUrlKey.Equals(childUrl)).Id;
            var versioning = new Versioning();
            versioning.RootAdId = rootId;
            versioning.ChildAdId = childId;
            context.Versioning.Add(versioning);
            context.SaveChanges();
            return ResponseStatuses.SUCCESS;
        }


        public string SaveAdVersioningByUrls(string rootUrl, string childUrl)
        {
            throw new NotImplementedException();
        }


        public string SaveAdVersioningByPreviousIdAndUrl(int prevId, string childUrl)
        {
            int childId = context.SimpleAds.FirstOrDefault(a => a.ShortUrlKey.Equals(childUrl)).Id;
            var rootVersion = context.Versioning.FirstOrDefault(a => a.ChildAdId == prevId);
            int rootId = prevId;
            if (rootVersion != null)
            {
                rootId = rootVersion.Id;
            }
            var versioning = new Versioning();
            versioning.RootAdId = rootId;
            versioning.ChildAdId = childId;
            context.Versioning.Add(versioning);
            context.SaveChanges();
            return ResponseStatuses.SUCCESS;
        }


        public string SaveAbTest(ABTest test)
        {
            context.AbTests.Add(test);
            context.SaveChanges();
            return ResponseStatuses.SUCCESS;
        }
        [ReferencePreservingDataContractFormatAttribute]
        public ABTest GetAbTestByUrl(string url)
        {
            return context.AbTests
                .Include("AdA")
                .Include("AdA.StateGraph")
                .Include("AdA.AdStates")
                .Include("AdA.AdStates.VideoUnit")
                .Include("AdA.AdStates.UserElements")
                .Include("AdB")
                .Include("AdB.StateGraph")
                .Include("AdB.AdStates")
                .Include("AdB.AdStates.VideoUnit")
                .Include("AdB.AdStates.UserElements")
                .FirstOrDefault(a => a.Url.Equals(url));
        }
        [ReferencePreservingDataContractFormatAttribute]
        public ABTest GetAbTestById(int id)
        {
            return context.AbTests
                .Include("AdA")
                .Include("AdA.StateGraph")
                .Include("AdA.AdStates")
                .Include("AdA.AdStates.VideoUnit")
                .Include("AdA.AdStates.UserElements")
                .Include("AdB")
                .Include("AdB.StateGraph")
                .Include("AdB.AdStates")
                .Include("AdB.AdStates.VideoUnit")
                .Include("AdB.AdStates.UserElements")
                .Include("AdA.AdSessions")
                .Include("AdA.AdSessions.Activities")
                .Include("AdA.AdSessions.Activities.Clicks")
                .Include("AdB.AdSessions")
                .Include("AdB.AdSessions.Activities")
                .Include("AdB.AdSessions.Activities.Clicks")
                .FirstOrDefault(a=>a.Id==id);
        }


        public string SetActiveByAdId(int id)
        {
            var current = context.SimpleAds.Find(id);
            var active = context.SimpleAds.FirstOrDefault(a => a.ShortUrlKey.Equals(current.ShortUrlKey) && a.IsActive);
            if (active != null)
            {
                active.IsActive = false;
                context.Entry(active).State = System.Data.Entity.EntityState.Modified;
            }
            current.IsActive = true;
            context.Entry(current).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return ResponseStatuses.SUCCESS;
        }

        [ReferencePreservingDataContractFormatAttribute]
        public IEnumerable<ABTest> GetAllActiveTests(string UserId)
        {
            var tests = context.AbTests.Include("AdA").Include("AdB").Where(a => a.AdA.UserId.Equals(UserId));
            foreach (var t in tests)
            {
                t.AdA.ToDTO();
                t.AdB.ToDTO();
            }
            return tests;
        }


        public void RemoveAdById(int id)
        {
            SimpleAdModel ad = context.SimpleAds.Find(id);
            var abtests = context.AbTests.Where(a => a.AdAId == id || a.AdBId == id);
            foreach (var ab in abtests)
            {
                context.AbTests.Remove(ab);
            }
            context.SimpleAds.Remove(ad);
            context.SaveChanges();
        }

        public void RemoveAdByUrl(string url)
        {
            var ads = context.SimpleAds.Where(a=>a.ShortUrlKey.Equals(url));
            var abtests = context.AbTests.Where(a => ads.Select(b => b.Id).Contains(a.AdAId.Value) ||
                ads.Select(b => b.Id).Contains(a.AdBId.Value));
            foreach (var ad in ads)
            {
                context.SimpleAds.Remove(ad);
            }
            foreach(var ab in abtests)
            {
                context.AbTests.Remove(ab);
            }
            context.SaveChanges();
        }


        public void RemoveAbTestById(int id)
        {
            ABTest ab = context.AbTests.Find(id);
            context.AbTests.Remove(ab);
            context.SaveChanges();
        }
    }
}
