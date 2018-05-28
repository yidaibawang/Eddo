using System;
using System.Threading.Tasks;
using Web.Core.Order;
using Eddo.Domain.Repository;
using Eddo.Applications.Services;
using System.Collections.Generic;

namespace Web.Application.Order
{
    public class OrderInfoAppService : ApplicationService,IOrderInfoAppService
    {    

        private readonly IRepository<OrderInfo> _orderInfoRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        public OrderInfoAppService(IRepository<OrderInfo> orderInfoRepository, IRepository<OrderDetail> orderDetailRepository)
        {
            _orderInfoRepository = orderInfoRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task CreateOrUpdateOrderInfo(OrderInfo input)
        {
            using (var uw = UnitOfWorkManager.Begin())
            {
                OrderInfo ordrinfo=_orderInfoRepository.Add(input);
                await uw.CommitAsync();
            }
        }
        public async Task<IList<OrderInfo>> GetAll()
        {
            IList<OrderInfo> ordrinfo = await _orderInfoRepository.GetAllListAsync();
            return ordrinfo;
        }
        public async Task delete(int id)
        {
            await _orderInfoRepository.RemoveAsync(id);
        }
    }
}
