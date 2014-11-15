using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImpulseApp.Models.ComplexViewModels
{
    public class ExtendedActivity
    {
        public ExtendedActivity()
        {
            ClicksTemp = new List<StatModels.Click>();
        }
        public StatModels.Activity Activity { get; set; }
        public List<StatModels.Click> ClicksTemp { get; set; }
    }
    public class AdOutboundModel
    {
        public StatModels.AdSession Session {get; set;}
        public List<ExtendedActivity> ActivitiesTemp { get; set; }
        public ExtendedActivity CurrentActivity { get; set; }
        
        public String htmlString { get; set; }

        public void CreateCurrentActivity(StatModels.Activity activity)
        {
            CurrentActivity = new ExtendedActivity();
            CurrentActivity.Activity = activity;
        }

        public void EndActivity()
        {
            CurrentActivity.Activity.EndTime = DateTime.Now;
            ActivitiesTemp.Add(CurrentActivity);
            CurrentActivity = null;
        }
    }
}