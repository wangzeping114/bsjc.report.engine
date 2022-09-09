using Microsoft.AspNetCore.Mvc;
using N6.Bsjc.Reporting.Domain;

namespace N6.Bsjc.ReportDocumentViewer.Controllers
{
    public class DisplayReportController : Controller
    {
        public IActionResult Index(ReportingControlModel model)
        {
            return View(model);
        }
    }
}
