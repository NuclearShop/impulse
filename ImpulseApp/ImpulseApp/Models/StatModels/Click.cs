using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImpulseApp.Models.StatModels
{
    public class Click
    {
        [Key]
        public int ClickId { get; set; }
        public virtual int ActivityId { get; set; }
        [ForeignKey("ActivityId")]
        public virtual Activity Activity { get; set; }
        public string ClickZone { get; set; }
        public DateTime ClickTime { get; set; }
        public int ClickType { get; set; }
    }
}