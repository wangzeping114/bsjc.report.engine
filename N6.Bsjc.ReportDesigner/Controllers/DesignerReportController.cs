using Microsoft.AspNetCore.Mvc;
using N6.Bsjc.Reporting.Domain;

namespace N6.Bsjc.ReportDesigner.Controllers
{
    public class DesignerReportController : Controller
    {
        public IActionResult Index(ReportingControlModel model)
        {
            return View(model);
        }
    }
}
