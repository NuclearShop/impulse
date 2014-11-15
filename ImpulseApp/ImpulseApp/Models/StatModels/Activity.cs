using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImpulseApp.Models.StatModels
{
    public class Activity
    {
        public Activity()
        {
            this.Clicks = new HashSet<Click>();
        }
        [Key]
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public virtual int SessionId { get; set; }

        [ForeignKey("SessionId")]
        public virtual AdSession Session { get; set; }

        public virtual ICollection<Click> Clicks { get; set; }
    }
}