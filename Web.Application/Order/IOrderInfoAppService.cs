using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Applications.Services;
using Web.Core.Order;

namespace Web.Application.Order
{
    public interface IOrderInfoAppService:IApplicationService
    {
        Task CreateOrUpdateOrderInfo(OrderInfo input);

        Task delete(int id);

        Task<IList<OrderInfo>> GetAll();
    }
}
