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
    public class AdStateNotFoundException:ImpulseBaseException
    {
        public AdStateNotFoundException(string message):base(message)
        {
        }
    }
}
