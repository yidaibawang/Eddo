using System.Web.Mvc;
using Web.Core;
using System.Threading.Tasks;
using Eddo.Web.Mvc.Controllers;
using Web.Application.Order;
using Web.Core.Order;
using System.Collections.Generic;

namespace WebDemo.Controllers
{

    public class HomeController : EddoController
    {

        private readonly IOrderInfoAppService OrderInfoAppService;
        public HomeController(IOrderInfoAppService orderInfoAppService)
        {
            OrderInfoAppService = orderInfoAppService;
        }
        public async Task<ActionResult> Index()
        {
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.No = System.Guid.NewGuid().ToString("N");
            orderInfo.ProductCount = 3;
            orderInfo.OrderDate = System.DateTime.Now;
            orderInfo.Remark = "123";
            orderInfo.Status = "1";

            OrderDetail OrderDetail1 = new OrderDetail();
            OrderDetail1.OrderInfoId = orderInfo.Id;
            OrderDetail1.ProductId = 1;
            OrderDetail1.ProductName = "苹果";

            OrderDetail OrderDetail2 = new OrderDetail();
            OrderDetail2.OrderInfoId = orderInfo.Id;
            OrderDetail2.ProductId = 2;
            OrderDetail2.ProductName = "橘子";

            OrderDetail OrderDetail3 = new OrderDetail();
            OrderDetail3.OrderInfoId = orderInfo.Id;
            OrderDetail3.ProductId = 3;
            OrderDetail3.ProductName = "柚子";
            orderInfo.Details.Add(OrderDetail1);
            orderInfo.Details.Add(OrderDetail2);
            orderInfo.Details.Add(OrderDetail3);
            await OrderInfoAppService.CreateOrUpdateOrderInfo(orderInfo);
            IList<OrderInfo> listdata = await OrderInfoAppService.GetAll();
            return View(listdata);

        }
        

        public async Task<JsonResult> Add(User user)
        {

            return Json(null);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}