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
    }

    public class CompareChartModel
    {
        public CompareChartModel()
        {
            charts = new List<StatChartJS>();
        }
        public List<StatChartJS> charts { get; set; }
    }
}