using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoSoft.Web.Controllers
{   
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {   
            
            return RedirectToRoute(new { area="Admin", controller = "Home" ,action="Index"});
        }
    }
}