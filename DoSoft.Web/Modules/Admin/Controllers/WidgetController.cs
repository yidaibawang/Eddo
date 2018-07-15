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
        [ChildActionOnly]
        public virtual ActionResult WidgetsByZone(string widgetZone)
        {
      
          

            return PartialView(widgetZone);
        }
    }
}