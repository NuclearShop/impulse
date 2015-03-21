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
    public class AdState
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int AdId { get; set; }
        [DataMember]
        public int StartTime { get; set; }
        [DataMember]
        public int EndTime { get; set; }
        [DataMember]
        public string HtmlSource { get; set; }
        [DataMember]
        public int VideoUnitId { get; set; }

        [ForeignKey("AdId")]
        [DataMember]
        public SimpleAdModel Ad { get; set; }
        [DataMember]
        [ForeignKey("VideoUnitId")]
        public virtual VideoUnit VideoUnit { get; set; }
    }
}
