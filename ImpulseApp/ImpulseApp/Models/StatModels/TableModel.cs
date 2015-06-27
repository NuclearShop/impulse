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

    public class UniqueVisitorsTable
    {
        [Display(Name="Период")]
        public string Date { get; set; }
        [Display(Name = "Уникальные посети")]
        public int UniqueVisitors { get; set; }
        [Display(Name = "Среднее время")]
        public string AverageTime { get; set; }
        [Display(Name = "Максимальное время")]
        public string MaxTime { get; set; }
    }

    public class LocaleVisitorsTable
    {
        [Display(Name = "Период")]
        public string Locale { get; set; }
        [Display(Name = "Просмотры за неделю")]
        public string ViewsByWeek { get; set; }
        [Display(Name = "Просмотры за неделю")]
        public string ViewsByMonth { get; set; }
        [Display(Name = "Самая популярная презентация")]
        public string PopularPresentation { get; set; }
    }
}