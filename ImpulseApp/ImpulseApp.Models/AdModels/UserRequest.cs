using ImpulseApp.Models.StatModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.AdModels
{
    public class UserRequest
    {
        [Key]
        public int Id { get; set; }

        public int AdId { get; set; }

        public string MainText { get; set; }

        public string BaseUrl { get; set; }

        public string UserIp { get; set; }

        public DateTime DateTime { get; set; }

        public string AdditionalText { get; set; }

        [ForeignKey("AdId")]
        public SimpleAdModel Ad { get; set; }

    }
}
