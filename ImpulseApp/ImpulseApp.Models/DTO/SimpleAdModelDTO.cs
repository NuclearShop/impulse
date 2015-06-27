using ImpulseApp.Models.AdModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.DTO
{
    public class SimpleAdModelDTO
    {
        public SimpleAdModelDTO()
        {
            AdStates = new List<AdStateDTO>();
            StateGraph = new List<NodeLinkDTO>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string HtmlStartSource { get; set; }
        public string HtmlEndSource { get; set; }
        public string HtmlSource { get; set; }
        public string ShortUrlKey { get; set; }
        public string Poster { get; set; }
        public int FirstState { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsRoot { get; set; }
        public bool IsActive { get; set; }
        public List<AdStateDTO> AdStates { get; set; }
        public List<NodeLinkDTO> StateGraph { get; set; }
    }
}
