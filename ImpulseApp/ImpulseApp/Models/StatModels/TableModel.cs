using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImpulseApp.Models.StatModels
{
    public class TableRowModel
    {
        [Display(Name="IP")]
        public string IP { get; set; }
        [Display(Name = "Браузер")]
        public string Browser { get; set; }
        [Display(Name = "Первый этап")]
        public string FirstStage { get; set; }
        [Display(Name = "Второй этап")]
        public string SecondStage { get; set; }
        [Display(Name = "Третий этап")]
        public string ThirdStage { get; set; }
        [Display(Name = "Локаль")]
        public string Locale { get; set; }
    }
}