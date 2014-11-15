using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImpulseApp.Models.StatModels
{
    public class Dictionary
    {
        public enum Locale
        {
            RU = 1,
            EN = 2,
            US = 3,
            GE = 4
        };

        public enum Browser
        {
            WebKit = 1,
            Moz = 2,
            IE = 3,
            Opera = 4
        }

        public enum ClickType
        {
            NextSlide = 1,
            PreviousSlide = 2,
            NoAction = 3,
            Redirect = 4,
            Activate = 5
        }
    }
}