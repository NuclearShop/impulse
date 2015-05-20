using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.Exceptions
{
    [Serializable]
    [DataContract]
    public class VersioningException : ImpulseBaseException
    {
        public VersioningException(string message):base(message)
        {

        }
    }
}
