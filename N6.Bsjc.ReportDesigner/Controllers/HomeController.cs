using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using N6.Bsjc.ReportDesigner.Models;
using N6.Bsjc.Reporting.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace N6.Bsjc.ReportDesigner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new ReportingControlModel { Id = "554b21ca-8291-943c-36b3-3a0604f0d960", Title = "财务报表设计器" };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
