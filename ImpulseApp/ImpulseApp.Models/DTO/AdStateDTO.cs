using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.DTO
{
    public class AdStateDTO
    {
        public AdStateDTO()
        {
            UserElements = new HashSet<UserElementDTO>();
        }
        public int Id { get; set; }
        public int EndTime { get; set; }
        public bool IsFullPlay { get; set; }
        public string Name { get; set; }
        public string ChainedHtml { get; set; }
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
        public int VideoUnitId { get; set; }
        public int DefaultNext { get; set; }
        public int DefaultNextTime { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public VideoUnitDTO VideoUnit { get; set; }
        public HashSet<UserElementDTO> UserElements { get; set; }
    }
}
