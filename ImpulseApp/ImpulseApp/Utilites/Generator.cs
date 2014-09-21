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
            for (int i = 0; i<length; i++)
            {
                Random r = new Random();
                s.Append(r.Next(0, 10));
            }
            return s.ToString();
        }
    }
}