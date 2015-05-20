using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.UtilModels
{
    public class ModeratorView
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime ViewDateTime { get; set; }
        [DataMember]
        public int AdId { get; set; }
        [DataMember]
        public bool IsReviewed { get; set; }
        [DataMember]
        public bool IsApproved { get; set; }
        [DataMember]
        public string Review { get; set; }
    }
}
