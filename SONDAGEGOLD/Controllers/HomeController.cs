using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SONDAGEGOLD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sondange()
        {
            return View();
        }
        public ActionResult Vote()
        {
            return View();
        }
        public ActionResult Resultat()
        {
            return View();
        }
    }
}