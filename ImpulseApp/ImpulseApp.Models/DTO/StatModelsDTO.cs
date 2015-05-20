using ImpulseApp.Models.StatModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.DTO
{
    class SessionStatModel
    {
        public Activity CurrentActivity { get; set; }
        public List<Activity> Activities { get; set; }
        public int AdId { get; set; }
        public DateTime AdTimeStart { get; set; }
        public DateTime AdTimeEnd { get; set; }
    }


}
