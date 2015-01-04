using ImpulseApp.Models.AdModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImpulseApp.Models.StatModels
{
    public class StatChartJS
    {
        public StatChartJS()
        {
            labels = new List<string>();
            data = new List<string>();
        }
        public List<String> labels { get; set; }
        public List<String> data { get; set; }
        public String name { get; set; }
        public String color { get; set; }
    }

    public class CompareChartModel
    {
        public CompareChartModel()
        {
            charts = new List<StatChartJS>();
        }
        public List<StatChartJS> charts { get; set; }
    }

    public class DetailedStatisticsModel
    {
        public DetailedStatisticsModel()
        { }
        
    }
}