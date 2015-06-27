using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.DTO
{
    public class VideoUnitDTO
    {
        public int Id { get; set; }
        public DateTime DateLoaded { get; set; }
        public String Name { get; set; }
        public String GeneratedName { get; set; }
        public String UserName { get; set; }
        public float Length { get; set; }
        public string FullPath { get; set; }
        public string MimeType { get; set; }
        public string Image { get; set; }
    }
}
