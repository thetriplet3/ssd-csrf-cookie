using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using csrf_cookie.Common;
using csrf_cookie.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csrf_cookie.Controllers
{
    public class LoginController : Controller
    {
        TokenHandler token;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(User user)
        {
            if (user.Username == Properties.Values.DEFAULT_USERNAME && user.Password == Properties.Values.DEFAULT_PASSWORD)
            {
                token = new TokenHandler();
                HttpContext.Session.SetString(Properties.Values.SESSION_KEY, HttpContext.Session.Id);
                token.GenerateCSRFToken(HttpContext.Session.Id);
                Response.Cookies.Append("CSRF-TOKEN", token.GetCSRFToken(HttpContext.Session.Id));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Message"] = "Incorrect username/password";
                return View("Index");
            }
        }

        [HttpGet("Login/Token")]
        public ActionResult<String> GetCSRFToken()
        {
            var value = HttpContext.Session.GetString(Properties.Values.SESSION_KEY);
            token = new TokenHandler();
            return token.GetCSRFToken(HttpContext.Session.Id);
        }
    }
}