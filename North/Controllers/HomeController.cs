using North.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace North.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var kategoriler = new northwindEntities().Categories.ToList();
            return View(kategoriler);
        }
    }
}