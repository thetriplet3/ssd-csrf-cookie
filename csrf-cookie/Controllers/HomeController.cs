using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using csrf_cookie.Models;
using Microsoft.AspNetCore.Http;
using csrf_cookie.Common;

namespace csrf_cookie.Controllers
{
    public class HomeController : Controller
    {
        private TokenHandler token;

        public IActionResult Index()
        {
            string sessionValue = HttpContext.Session.GetString(Properties.Values.SESSION_KEY);
            if (string.IsNullOrEmpty(sessionValue))
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            else
            {
                return View();
            }
        }

        [HttpPost("/")]
        public ActionResult Post(Home obj)
        {
            token = new TokenHandler();
            string csrfToken = token.GetCSRFToken(HttpContext.Session.Id);

            if (csrfToken == obj.Token)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new String("Invalid Token"));
            }

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
