using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ImpulseApp.Utilites
{
    public class ImpulseFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public ImpulseFormDataStreamProvider(string uploadPath)
        : base(uploadPath)
    {
 
    }
 
    public override string GetLocalFileName(HttpContentHeaders headers)
    {
      string fileName = headers.ContentDisposition.FileName;
      if(string.IsNullOrWhiteSpace(fileName))
      {
        fileName = Guid.NewGuid().ToString() + ".data";
      }
      return fileName.Replace("\"", string.Empty);
    }
    }
}
