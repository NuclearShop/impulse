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
            this.StateGraph = new HashSet<NodeLink>();
        }
        public SimpleAdModel(string UserId)
            : this()
        {

            this.UserId = UserId;

        }
        public void Init(string userId = null)
        {
            this.IsRoot = false;
            this.IsActive = false;
            this.DateTime = DateTime.Now;
            this.Id = 0;
            if (!String.IsNullOrWhiteSpace(userId))
            {
                this.UserId = userId;
            } 
            if (String.IsNullOrEmpty(ShortUrlKey) || String.IsNullOrWhiteSpace(ShortUrlKey))
            {
                this.ShortUrlKey = Generator.GenerateShortAdUrl();
                this.IsRoot = true;
                this.IsActive = true;
            }
            this.AdSessions = new HashSet<AdSession>();
            if (AdStates == null)
                this.AdStates = new HashSet<AdState>();
            foreach (var state in this.AdStates)
            {
                state.Init();
            }
            this.Name = "Реклама " + ShortUrlKey;

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
        public string HtmlStartSource { get; set; }
        [DataMember]
        public string HtmlEndSource { get; set; }
        [DataMember]
        public virtual string UserId { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember]
        public bool IsRoot { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public virtual HashSet<AdSession> AdSessions { get; set; }
        [DataMember]
        public virtual HashSet<AdState> AdStates { get; set; }
        [DataMember]
        public virtual HashSet<NodeLink> StateGraph { get; set; }

    }
}