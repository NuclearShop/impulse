using ImpulseApp.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.Exception
{
    [Serializable]
    [DataContract]
    public class MultiplyAdStatesFoundException : ImpulseBaseException
    {
        public MultiplyAdStatesFoundException(string message)
            : base(message)
        {
        }
    }
}
