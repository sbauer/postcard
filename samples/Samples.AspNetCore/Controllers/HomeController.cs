using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Postcard;
using Samples.AspNetCore.Models;

namespace Samples.AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailer _mailer;

        public HomeController(IMailer mailer)
        {
            _mailer = mailer;
        }
        public async Task<IActionResult> Index()
        {
            await _mailer.SendAsync(new WelcomeEmail());
            return Content((_mailer != null).ToString());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}