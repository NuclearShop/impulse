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
    public class ImpulseBaseException
    {
        protected string _message;

        public ImpulseBaseException(string message)
        {
            _message = message;
        }

        [DataMember]
        public string Message { get { return _message; } set { _message = value; } }
    }
}
