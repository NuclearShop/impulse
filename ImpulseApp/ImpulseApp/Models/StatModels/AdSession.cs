using ImpulseApp.Models.AdModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImpulseApp.Models.StatModels
{
    public class AdSession
    {
        public AdSession()
        {
            this.Activities = new HashSet<Activity>();
        }
        [Key]
        public int SessionId { get; set; }

        public virtual int AdId { get; set; }

        public DateTime DateTimeStart { get; set; }

        public DateTime DateTimeEnd { get; set; }

        public int ActiveMilliseconds { get; set; }

        public string UserIp { get; set; }

        public string UserLocation { get; set; }

        public string UserLocale { get; set; }

        public string UserBrowser { get; set; }

        [ForeignKey("AdId")]
        public virtual SimpleAdModel Ad { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}