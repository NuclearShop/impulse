using ImpulseApp.Models.AdModels;
using ImpulseApp.Outbound.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Serialization;

namespace ImpulseApp.Controllers.APIControllers
{
    public class VASTExportController : ApiController
    {
        DBService.IDBService service = new DBService.DBServiceClient();
        public string ToXML(Outbound.ContractTypes.VMAP vmap)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(vmap.GetType());
            serializer.Serialize(stringwriter, vmap);
            return stringwriter.ToString();
        }
        [Route("api/vast/{id}")]
        public async Task<HttpResponseMessage> GetVMAP(int id)
        {
            SimpleAdModel ad = await service.GetAdByIdAsync(id);
            Outbound.ContractTypes.VMAP vmap = VMAPConverter.GetVMAP(ad);
            return Request.CreateResponse(HttpStatusCode.OK, ToXML(vmap));
        }
    }
}
