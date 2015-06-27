using ImpulseApp.Models.AdModels;
using ImpulseApp.Models.Utilites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ImpulseApp.Models.StatModels
{
    [DataContract]
    public class AdSession
    {
        public AdSession()
        {
            this.Activities = new HashSet<Activity>();
            
        }
        [DataMember]
        [Key]
        public int SessionId { get; set; }
        [DataMember]
        public virtual int AdId { get; set; }
        [DataMember]
        public DateTime DateTimeStart { get; set; }
        [DataMember]
        public DateTime DateTimeEnd { get; set; }
        [DataMember]
        public int ActiveMilliseconds { get; set; }
        [DataMember]
        public string UserIp { get; set; }
        [DataMember]
        public string UserLocation { get; set; }
        [DataMember]
        public string UserLocale { get; set; }
        [DataMember]
        public string UserBrowser { get; set; }
        [DataMember]
        public int? AbTestId { get; set; }
        [DataMember]
        [ForeignKey("AdId")]
        public virtual SimpleAdModel Ad { get; set; }
        [DataMember]
        public virtual HashSet<Activity> Activities { get; set; }
        
    }
}