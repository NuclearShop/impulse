using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ImpulseApp.Utilites
{
    public class Generator
    {
        public static string GenerateShortAdUrl(int length = 5)
        {
            StringBuilder s = new StringBuilder(length);
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i<length; i++)
            {
                
                s.Append(r.Next(0, 10));
            }
            return s.ToString();
        }
    }
}