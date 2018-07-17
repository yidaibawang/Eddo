using DoSoft.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoSoft.Admin.Controllers
{
    public class WidgetController:Controller
    {
        
        public WidgetController()
        {

        }
        [ChildActionOnly]
        public virtual ActionResult WidgetsByZone(string widgetZone)
        {
            
            return PartialView(widgetZone);
        }
        public virtual ActionResult Men(string widgetZone)
        {

            return PartialView("Menu");
        }
    }
}