using ImpulseApp.DBService;
using ImpulseApp.Models.AdModels;
using ImpulseApp.Models.Utilites;
using ImpulseApp.Utilites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ImpulseApp.Controllers.APIControllers
{
    public class UploadController : ApiController
    {
        IDBService service = new DBServiceClient();
        [Route("api/upload/video")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostFormData()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            VideoUnit video = new VideoUnit
            {
                DateLoaded = DateTime.Now,
                GeneratedName = Guid.NewGuid().GetHashCode().ToString(),
                Name = "DefaultName",
                UserName = User.Identity.Name
            };

            

            string root = HttpContext.Current.Server.MapPath("~/Videos");
            var path = "/" + User.Identity.Name + "/" + video.GeneratedName+"/";
            DirectoryInfo directory = Directory.CreateDirectory(root + path);
            var fileName = "Videos" + path;
            var provider = new ImpulseFormDataStreamProvider(directory.FullName);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (var file in provider.FileData)
                {
                    if (file.Headers.ContentType.MediaType.Contains("video"))
                    {
                        video.FullPath = fileName+Path.GetFileName(file.LocalFileName);
                        video.MimeType = file.Headers.ContentType.MediaType;
                    }
                    else
                    {
                        var exceptionMessage = new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType)
                        {
                            ReasonPhrase = "Файл не является видео",
                            Content = new StringContent("Тип загруженного объекта "+file.Headers.ContentType.MediaType)
                        };
                        throw new HttpResponseException(exceptionMessage);
                    }
                }
                await service.SaveVideoAsync(video);

                return Request.CreateResponse(HttpStatusCode.OK, video);
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(e.Response.StatusCode, e.Response.ReasonPhrase);
            }
        }
    }
}
