using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvioTravelling.Controllers
{
    public class GeoController : Controller
    {
        //Geo/EdiCIty?Id=1234
        public ActionResult EditCity(string Id)
        {
            ViewBag.EntityId = Id;
            return View();
        }

        public ActionResult Cites()
        {
            return View();
        }

        public ActionResult Showplaces()
        {
            return View();
        }

        public ActionResult EditShowplace(string Id)
        {
            ViewBag.EntityId = Id;
            return View();
        }

        public ActionResult Countries()
        {
            return View();
        }

        public ActionResult NewCountry()
        {
            return View();
        }

        public ActionResult EditCountry(string Id)
        {
            ViewBag.EntityId = Id;
            return View();
        }
    }
}