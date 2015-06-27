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
        public static string[] COUNTRY_NAMES = new String[] { "NO", "SE", "DK", "DE", "NL", "BE", "LU", "ES", "FR", "PL", 
            "CZ", "AT", "CH", "LI", "SK", "HU",
                    "SI", "IT", "SM", "HR", "BA", "YF", "ME", "AL", "MK","FI", "EE", "LV", "LT", "BY", 
                    "UA", "MD", "RO", "BG", "GR", "TR", "CY","RU"
        };
        public static string[] NAMES = new String[] { "ru-sc", "ru-kr", "ru-2485", "ru-ar", "ru-nn", "ru-yn", "ru-ky", "ru-ck", 
            "ru-kh", "ru-sl", "ru-ka", "ru-kt", "ru-ms", "ru-rz", "ru-sa", "ru-ul", 
            "ru-om", "ru-ns", "ru-mm", "ru-ln", "ru-sp", "ru-ki", "ru-kc", "ru-in", 
            "ru-kb", "ru-no", "ru-st", "ru-sm", "ru-ps", "ru-tv", "ru-vo", "ru-iv", 
            "ru-ys", "ru-kg", "ru-br", "ru-ks", "ru-lp", "ru-2509", "ru-ol", "ru-nz", 
            "ru-pz", "ru-vl", "ru-vr", "ru-ko", "ru-sv", "ru-bk", "ru-ud", "ru-mr", "ru-cv", 
            "ru-cl", "ru-ob", "ru-sr", "ru-tt", "ru-to", "ru-ty", "ru-ga", "ru-kk", "ru-cn", 
            "ru-kl", "ru-da", "ru-ro", "ru-bl", "ru-tu", "ru-ir", "ru-ct", "ru-yv", "ru-am", "ru-tb", "ru-tl", 
            "ru-ng", "ru-vg", "ru-kv", "ru-me", "ru-ke", "ru-as", "ru-pr", "ru-mg", "ru-bu", "ru-kn", "ru-kd", 
            "ru-ku", "ru-al", "ru-km", "ru-pe", "ru-ad" };
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
