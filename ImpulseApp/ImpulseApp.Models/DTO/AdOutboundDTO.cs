using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.DTO
{
    public class AdOutboundDTO
    {
        public int AdId { get; set; }
        public string AdUrl { get; set; }
    }
    public class AbOutboundDTO
    {
        public int AdIdA { get; set; }
        public int AdIdB { get; set; }
        public int AdIdCurrent { get; set; }
        public string AdUrlCurrent {get; set;}
        public int AbTestId { get; set; }
        public string AbTestUrl { get; set; }
    }
}
