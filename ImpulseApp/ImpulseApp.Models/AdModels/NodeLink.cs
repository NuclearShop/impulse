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
    public class NodeLink
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int AdId { get; set; }
        [DataMember]
        public int V1 { get; set; }
        [DataMember]
        public int V2 { get; set; }
        [DataMember]
        public int T { get; set; }

        [DataMember]
        [ForeignKey("AdId")]
        public SimpleAdModel AdModel { get; set; }
    }
}
