using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.AdModels
{
    public class Versioning
    {
        public Versioning()
        {
            Date = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public int RootAdId { get; set; }
        public int ChildAdId { get; set; }
        public DateTime Date { get; set; }

        /*[ForeignKey("RootAdId")]
        public SimpleAdModel RootAd { get; set; }
        [ForeignKey("ChildAdId")]
        public SimpleAdModel ChildAd { get; set; }*/
    }
}
