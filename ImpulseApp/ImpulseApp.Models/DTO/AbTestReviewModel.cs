using ImpulseApp.Models.AdModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.DTO
{
    public class AbTestReviewModelDTO
    {
        public AbTestReviewModelDTO()
        {
            ClickChart = new List<AbCompareClickChart>();
        }
        public int OverallClicksA { get; set; }
        public int OverallClicksB { get; set; }
        public int OverallDepthA { get; set; }
        public int OverallDepthB { get; set; }
        public SimpleAdModelDTO AdA { get; set; }
        public SimpleAdModelDTO AdB { get; set; }
        public ABTest AbTest { get; set; }
        public List<AbCompareClickChart> ClickChart { get; set; }
    }
    public class AbCompareClickChart
    {
        public int Iteration { get; set; }
        public int AdAClicks { get; set; }
        public int AdBClicks { get; set; }
    }
}
