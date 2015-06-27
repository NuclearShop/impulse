using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.DTO
{
    public class UserRequestDTO
    {
        public int Id { get; set; }

        public int AdId { get; set; }

        public string MainText { get; set; }

        public string BaseUrl { get; set; }

        public string UserIp { get; set; }

        public DateTime DateTime { get; set; }

        public string AdditionalText { get; set; }
    }
}
