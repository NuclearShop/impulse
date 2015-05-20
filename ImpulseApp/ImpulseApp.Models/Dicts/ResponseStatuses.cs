using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.Dicts
{
    public class ResponseStatuses
    {
        public static string SUCCESS = "success";

        static string WARNING = "warning";
        static string ERROR = "error";

        public static string BuildErrorResponse(string msg)
        {
            return ERROR + ": " + msg;
        }
    }
    public class Browsers
    {
        public static string[] NAMES = new String[] { "Safari", "Opera", "Chrome", "Firefox", "IE", "Mobile" };
    }
    public class Locales
    {
        public static string[] NAMES = new String[] { "RU", "EN", "NO", "JP", "US" };
    }
    public class AdStateTypes
    {
        public const string FIRST = "FIRST";
        public const string MIDDLE = "START";
        public const string FINAL = "FINAL";

        public static int GetClickType(string adStateType)
        {
            switch (adStateType)
            {
                case FIRST: return 0;
                case MIDDLE: return 1;
                case FINAL: return 2;
                default: return -1;
            }
        }
    }
}
