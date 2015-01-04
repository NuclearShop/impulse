using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ImpulseApp.WCF
{
    [ServiceContract]
    public interface ICommonService
    {
        [OperationContract]
        ImpulseAppWCF.ContractTypes.VAST VASTRequest();

        [OperationContract]
        ImpulseAppWCF.ContractTypes.VMAP VMAPRequest();
    }
}
