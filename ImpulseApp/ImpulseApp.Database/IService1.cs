using ImpulseApp.Models.AdModels;
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


    }
}
