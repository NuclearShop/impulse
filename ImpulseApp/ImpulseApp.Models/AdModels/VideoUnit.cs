using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.AdModels
{
    [DataContract]
    public class VideoUnit
    {
        public VideoUnit()
        {
            this.AdStates = new HashSet<AdState>();
        }
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime DateLoaded { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public String GeneratedName {get; set;}

        [DataMember]
        public String UserName { get; set; }

        [DataMember]
        public int Length { get; set; }

        [DataMember]
        public string FullPath { get; set; }

        [DataMember]
        public string MimeType { get; set; }

        [DataMember]
        public String Image { get; set; }

        [DataMember]
        public virtual HashSet<AdState> AdStates { get; set; }
    }
}
