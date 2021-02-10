using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Moment2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Moment2.Controllers
{
    public class HomeController : Controller
    {


        public dynamic Index()
        {
            // get session variable from birthdaysController
            string time = HttpContext.Session.GetString("time");
            if (time != null)
            {
                string start = time.Remove(0, 3);
                // convert string time to int
                int starttime = Int32.Parse(start);

                DateTime now = DateTime.Now;
                string nowtime = now.ToString("HH:mm");
                string end = nowtime.Remove(0, 3);
                int nowint = Int32.Parse(end);

                int loggedintime = nowint - starttime;
                string inloggad = loggedintime.ToString();
                HttpContext.Session.SetString("inloggad", inloggad);

                ViewBag.inloggad = inloggad; // how many minutes have you been on page
                ViewBag.time = time; // session variable Logged in time
            }
            
           
            return View();
        }

        [HttpGet]
        [Route("/nyKompis")]
        public IActionResult Who()
        {
            return View();
        }

        [HttpPost]
        [Route("/nyKompis")]
        public IActionResult Who(People model)
        {
            if (ModelState.IsValid)
            {
                model.GetAge();
                model.CountDaysToBirthday();
                

                // Get existing json
                var JsonStr = System.IO.File.ReadAllText("birthdays.json");
                var JsonObj = JsonConvert.DeserializeObject<List<People>>(JsonStr);
                // Add new person
                JsonObj.Add(model);
                // Serialize and 
                System.IO.File.WriteAllText("birthdays.json", JsonConvert.SerializeObject(JsonObj, Formatting.Indented));
                // clear the form
                ModelState.Clear();

                // save person in session storage as serialized json
                var newPerson = JsonConvert.SerializeObject(model);
                HttpContext.Session.SetString("newPerson", newPerson);

                return RedirectToAction("Bchild");
            }
            return View();
        }

        [Route("/grattis")]
        public IActionResult Bchild()
        {
            string newPerson = HttpContext.Session.GetString("newPerson");

            People p = JsonConvert.DeserializeObject<People>(newPerson);

            ViewBag.p = p;

            return View();
        }

    }
}
