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
    public class Click
    {
        [Key]
        [DataMember]
        public int ClickId { get; set; }
        [DataMember]
        public virtual int ActivityId { get; set; }
        [ForeignKey("ActivityId")]
        [DataMember]
        public virtual Activity Activity { get; set; }
        [DataMember]
        public string ClickZone { get; set; }
        [DataMember]
        public DateTime ClickTime { get; set; }
        [DataMember]
        public string ClickType { get; set; }
        [DataMember]
        public int ClickCurrentStage { get; set; }
        [DataMember]
        public int ClickNextStage { get; set; }
        [DataMember]
        public int ClickNextTime { get; set; }
        [DataMember]
        public string ClickText { get; set; }
        [DataMember]
        public string ClickStamp { get; set; }
    }
}