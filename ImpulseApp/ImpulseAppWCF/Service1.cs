using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ImpulseAppWCF.ContractTypes;

namespace ImpulseApp.WCF
{
    public class CommonService : ICommonService
    {
        public ImpulseAppWCF.ContractTypes.VAST VASTRequest()
        {
            VAST v = new VAST();
            return v;
            
        }

        public ImpulseAppWCF.ContractTypes.VMAP VMAPRequest()
        {
            throw new NotImplementedException();
        }
    }
}
