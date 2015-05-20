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
    public class AdState
    {
        public AdState()
        {
            this.UserElements = new HashSet<UserElement>();
        }
        public void Init()
        {
            this.Id = 0;
            if (Name == null)
                Name = Guid.NewGuid().ToString();
        }
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
        public int EndTime { get; set; }
        [DataMember]
        public int X { get; set; }
        [DataMember]
        public int Y { get; set; }
        [DataMember]
        public string ChainedHtml { get; set; }
        [DataMember]
        public bool IsFullPlay { get; set; }
        [DataMember]
        public bool IsStart { get; set; }
        [DataMember]
        public int VideoUnitId { get; set; }
        [DataMember]
        public bool IsEnd { get; set; }
        [DataMember]
        public int DefaultNext { get; set; }
        [DataMember]
        public int DefaultNextTime { get; set; }
        

        [ForeignKey("AdId")]
        [DataMember]
        public SimpleAdModel Ad { get; set; }
        [DataMember]
        [ForeignKey("VideoUnitId")]
        public virtual VideoUnit VideoUnit { get; set; }
        [DataMember]
        public virtual HashSet<UserElement> UserElements { get; set; }
    }
}
