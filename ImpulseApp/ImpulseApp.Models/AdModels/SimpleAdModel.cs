using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ImpulseApp.Utilites;
using ImpulseApp.Models.StatModels;
using System.Runtime.Serialization;
using ImpulseApp.Models.Utilites;

namespace ImpulseApp.Models.AdModels
{
    [DataContract]
    public class SimpleAdModel
    {
        public SimpleAdModel()
        {
            ShortUrlKey = Generator.GenerateShortAdUrl();
            this.DateTime = DateTime.Now;
            this.AdSessions = new HashSet<AdSession>();
            this.AdStates = new HashSet<AdState>();
        }
        public SimpleAdModel(string UserId):this()
        {
            
            this.UserId = UserId;
            
        }
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public string ShortUrlKey { get; set; }
        [DataMember]
        public string HtmlSource { get; set; }
        [DataMember]
        public string JsSource { get; set; }
        [DataMember]
        public virtual string UserId { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }

        [DataMember]
        public virtual HashSet<AdSession> AdSessions { get; set; }
        [DataMember]
        public virtual HashSet<AdState> AdStates { get; set; }
        
    }
}