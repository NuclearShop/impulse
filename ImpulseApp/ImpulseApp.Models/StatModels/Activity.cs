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
    public class Activity
    {
        public Activity()
        {
            this.Clicks = new HashSet<Click>();
        }
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime StartTime { get; set; }
        [DataMember]
        public DateTime EndTime { get; set; }
        [DataMember]
        public virtual int SessionId { get; set; }
        [DataMember]
        public string CurrentStateName { get; set; }
        [DataMember]
        [ForeignKey("SessionId")]
        public virtual AdSession Session { get; set; }
        [DataMember]
        public virtual HashSet<Click> Clicks { get; set; }
    }
}