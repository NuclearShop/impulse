using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.AdModels
{
    [DataContract]
    public class ABTest
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int? AdAId { get; set; }

        [DataMember]
        public int? AdBId { get; set; }

        [DataMember]
        public DateTime DateStart { get; set; }

        [DataMember]
        public DateTime DateEnd { get; set; }

        [DataMember]
        public int ChangeHours { get; set; }

        [DataMember]
        public int ChangeCount { get; set; }

        [DataMember]
        public int ActiveAd { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        [ForeignKey("AdAId")]
        public SimpleAdModel AdA { get; set; }
        [DataMember]
        [ForeignKey("AdBId")]
        public SimpleAdModel AdB { get; set; }

    }
}
