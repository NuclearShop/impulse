using ImpulseApp.Models.Dicts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StubMethods
{
    public class StubUtils
    {
        public static string GenerateIP()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            String s = "";
            s += ((rand.Next(255) + ".") + (rand.Next(255) + ".") + (rand.Next(255) + ".") + (rand.Next(255)));
            return s;
        }

        public static string GenerateBrowser()
        {
            String[] browsers = Browsers.NAMES;
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            return browsers[rand.Next(browsers.Length)];
        }

        public static string GenerateLocale()
        {
            String[] locales = Locales.NAMES;
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            return locales[rand.Next(locales.Length)];
        }
        private static int curClick = -1;
        public static void InitClicks()
        {
            curClick = -1;
        }
        public static int GenerateClickType()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            if(rand.Next(10)>5 || curClick==2)
            {
                curClick = -1;
                return 0;
            }
            else
            {
                curClick++;
                return curClick;
            }
        }
    }
    
}