using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.DTO
{
    public class UserElementDTO
    {
        public UserElementDTO()
        {
            this.HtmlTags = new HashSet<HtmlTagDTO>();
        }
        public int Id { get; set; }
        public string HtmlId { get; set; }
        public string HtmlClass { get; set; }
        public string HtmlType { get; set; }
        public bool UseDefaultStyle { get; set; }
        public string HtmlStyle { get; set; }
        public string Text { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Action { get; set; }
        public int TimeAppear { get; set; }
        public int TimeDisappear { get; set; }
        public int CurrentId { get; set; }
        public int NextId { get; set; }
        public int NextTime { get; set; }
        public string FormName { get; set; }
        public HashSet<HtmlTagDTO> HtmlTags { get; set; }
    }

    public class HtmlTagDTO
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

}
