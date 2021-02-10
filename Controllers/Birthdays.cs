using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Moment2.Models;
using Microsoft.AspNetCore.Http;


namespace Moment2.Controllers
{
    public class Birthdays : Controller
    {
       
        public IActionResult Index()
        {
            // get session variable from birthdaysController
            string time = HttpContext.Session.GetString("time");
            if (time == null)
            {
                DateTime now = DateTime.Now;
                time = now.ToString("HH:mm");
                HttpContext.Session.SetString("time", time);
                ViewBag.time = time;
            }
           
            var JsonStr = System.IO.File.ReadAllText("birthdays.json");
            var JsonObj = JsonConvert.DeserializeObject<List<People>>(JsonStr);
            if(JsonObj.Count>0)
            {
                return View(JsonObj);
            }
            else
            {
                return View("Nofriends");
            }
        }

        public IActionResult Nofriends()
        {
            return View();
        }
    }
}
