﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.Exceptions
{
    [Serializable]
    [DataContract]
    public class VideoNotFoundException:ImpulseBaseException
    {
        public VideoNotFoundException(string message):base(message)
        {
        }

    }
}