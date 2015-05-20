using ImpulseApp.Models.StatModels;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ImpulseApp.Controllers.ExportControllers
{
    public class PdfExportController : Controller
    {
        private readonly HtmlViewRenderer htmlViewRenderer;
        private readonly StandardPdfRenderer standardPdfRenderer;

        public PdfExportController()
        {
            this.htmlViewRenderer = new HtmlViewRenderer();
            this.standardPdfRenderer = new StandardPdfRenderer();
        }
        [HttpPost]
        public ActionResult ViewPdf(string pageTitle, string htmlText)
        {
            //string htmlText = this.htmlViewRenderer.RenderViewToString(this, viewName, model);
            htmlText = System.Uri.UnescapeDataString(htmlText);
            htmlText = htmlText.Replace("%u", "\\u");
            htmlText = Regex.Unescape(htmlText);
            byte[] buffer = standardPdfRenderer.Render(htmlText, pageTitle);
            return File(buffer, "application/pdf", "export.pdf");
        }
	}
}