using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDemo.Controllers
{
    public class ErrorController : Controller
    {

        public ActionResult E404()
        {
            return View();
        }
    }
}