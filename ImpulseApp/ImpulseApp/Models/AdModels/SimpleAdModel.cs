﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ImpulseApp.Utilites;

namespace ImpulseApp.Models.AdModels
{
    public class SimpleAdModel
    {
        public SimpleAdModel()
        {
            ShortUrlKey = Generator.GenerateShortAdUrl();
            this.DateTime = DateTime.Now;
            UserId = HttpContext.Current.User.Identity.GetUserId();
        }
        [Key]
        public int Id { get; set; }

        public string ShortUrlKey { get; set; } 

        [AllowHtml]
        public string HtmlSource { get; set; }

        public string UserId { get; set; }

        public DateTime DateTime { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; } 
        
    }
}