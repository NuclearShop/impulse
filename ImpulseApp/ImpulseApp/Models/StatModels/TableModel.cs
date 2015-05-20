using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImpulseApp.Models.StatModels
{
    public class Stage
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class TableRowModel
    {
        public TableRowModel()
        {
            Stages = new HashSet<Stage>();
        }
        [Display(Name="IP")]
        public string IP { get; set; }
        [Display(Name = "Браузер")]
        public string Browser { get; set; }
        [Display(Name = "Локаль")]
        public string Locale { get; set; }
        public HashSet<Stage> Stages { get; set; }
    }
}