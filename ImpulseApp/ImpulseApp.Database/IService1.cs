using ImpulseApp.Models.AdModels;
using ImpulseApp.Models.DTO;
using ImpulseApp.Models.Exceptions;
using ImpulseApp.Models.StatModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ImpulseApp.Database
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IDBService
    {
        [OperationContract]
        IEnumerable<SimpleAdModel> GetUserAds(string UserId);
        [OperationContract]
        SimpleAdModel GetAdByUrl(string url);
        [OperationContract]
        SimpleAdModel GetAdById(int id);
        [OperationContract]
        string SaveAd(SimpleAdModel model, bool proceedToDB = true);

        [OperationContract]
        IEnumerable<AdSession> GetSessionsByAdId(int AdId);
        [OperationContract]
        string SaveAdSession(AdSession model, bool proceedToDB = true);

        [OperationContract]
        IEnumerable<Activity> GetActivityBySessionId(int SessionId);
        [OperationContract]
        string SaveActivity(Activity model, bool proceedToDB = true);

        [OperationContract]
        IEnumerable<Click> GetClicksByActivityId(int ActivityId);
        [OperationContract]
        string SaveClick(Click model, bool proceedToDB = true);
        [OperationContract]
        string SaveVideo(VideoUnit model);
        [OperationContract]
        IEnumerable<VideoUnit> GetUserVideo(string UserName);
        [OperationContract]
        [FaultContract(typeof(VideoNotFoundException))]
        VideoUnit GetVideoById(string UserName, int Id);

        [OperationContract]
        [FaultContract(typeof(VideoNotFoundException))]
        AdState GetStateByAdIdAndVideoId(int adId, int videoId, string stateName);

        [OperationContract]
        [FaultContract(typeof(VersioningException))]
        string SaveAdVersioningByEntity(Versioning versioning);

        [OperationContract]
        [FaultContract(typeof(VersioningException))]
        string SaveAdVersioningByIds(int rootId, int childId);

        [OperationContract]
        [FaultContract(typeof(VersioningException))]
        string SaveAdVersioningByIdAndUrl(int rootId, string childUrl);

        [OperationContract]
        [FaultContract(typeof(VersioningException))]
        string SaveAdVersioningByPreviousIdAndUrl(int prevId, string childUrl);

        [OperationContract]
        [FaultContract(typeof(VersioningException))]
        string SaveAdVersioningByUrls(string rootUrl, string childUrl);

        [OperationContract]
        [FaultContract(typeof(VersioningException))]
        Boolean IsRoot (int adId);

        [OperationContract]
        [FaultContract(typeof(VersioningException))]
        int GetRootAdId(int adId);

        [OperationContract]
        [FaultContract(typeof(VersioningException))]
        IEnumerable<VersioningDTO> GetChildAds(int adId);

        [OperationContract]
        [FaultContract(typeof(AbTestException))]
        string SaveAbTest(ABTest test);

        [OperationContract]
        [FaultContract(typeof(AbTestException))]
        ABTest GetAbTestByUrl(string url);

        [OperationContract]
        [FaultContract(typeof(AbTestException))]
        ABTest GetAbTestById(string id);

        [OperationContract]
        [FaultContract(typeof(VersioningException))]
        string SetActiveByAdId(int id);

    }
}
