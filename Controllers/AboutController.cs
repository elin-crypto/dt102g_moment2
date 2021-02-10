using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moment2.Controllers
{
    public class AboutController : Controller
    {
        [Route("/omsidan")]
        public IActionResult Index()
        {
            ViewBag.info1 = "På den här webbplatsen kan dina vänner lägga till sina födelsedagar och berätta vad de önskar sig.";
            ViewData["info2"] = "Det är ju kul!";
            return View();
        }
    }
}
