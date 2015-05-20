using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.AdModels
{
    [DataContract]
    public class UserElement
    {

        public UserElement()
        {
            this.HtmlTags = new HashSet<HtmlTag>();
        }

        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string HtmlId { get; set; }
        [DataMember]
        public string HtmlClass { get; set; }
        [DataMember]
        public string HtmlType { get; set; }
        [DataMember]
        public bool UseDefaultStyle { get; set; }
        [DataMember]
        public string HtmlStyle { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public int X { get; set; }
        [DataMember]
        public int Y { get; set; }
        [DataMember]
        public string Width { get; set; }
        [DataMember]
        public string Height { get; set; }
        [DataMember]
        public string Action { get; set; }
        [DataMember]
        public string FormName { get; set; }
        [DataMember]
        public int TimeAppear { get; set; }
        [DataMember]
        public int TimeDisappear { get; set; }
        [DataMember]
        public int CurrentId { get; set; }
        [DataMember]
        public int NextId { get; set; }
        [DataMember]
        public int NextTime { get; set; }
        [DataMember]
        public int AdStateId { get; set; }

        [ForeignKey("AdStateId")]
        [DataMember]
        public AdState AdState { get; set; }

        [DataMember]
        public virtual HashSet<HtmlTag> HtmlTags { get; set; }
    }

    public class HtmlTag
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public int UserElementId { get; set; }

        [DataMember]
        [ForeignKey("UserElementId")]
        public UserElement UserElement { get; set; }
    }
}
