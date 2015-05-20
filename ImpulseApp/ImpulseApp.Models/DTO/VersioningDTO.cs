using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.DTO
{
    public class VersioningDTO
    {
        public int RootAdId { get; set; }
        public int ChildAdId { get; set; }
        public DateTime date { get; set; }
    }
}
