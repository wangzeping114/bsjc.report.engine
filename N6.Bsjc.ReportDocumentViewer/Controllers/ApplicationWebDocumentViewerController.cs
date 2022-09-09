using DevExpress.AspNetCore.Reporting.WebDocumentViewer;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer.Native.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace N6.Bsjc.ReportDocumentViewer.Controllers
{
    [Route("DXXRDVMVC")]
    public class ApplicationWebDocumentViewerController : WebDocumentViewerController
    {
        public ApplicationWebDocumentViewerController(IWebDocumentViewerMvcControllerService controllerService)
            : base(controllerService)
        {
        }
        public override Task<IActionResult> Invoke()
        {
            return base.Invoke();
        }
    }
}
