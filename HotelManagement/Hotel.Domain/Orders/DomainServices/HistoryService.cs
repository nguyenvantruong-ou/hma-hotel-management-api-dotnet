using Hotel.Domain.Orders.DomainServices.Interfaces;
using Hotel.Domain.Orders.Entities;
using Hotel.Domain.Orders.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Orders.DomainServices
{
    public class HistoryService : IHistoryService
    {
        private IOrderRepository _repoOrder;
        private IOrderRoomRepository _repoRoomOrder;
        public HistoryService(IOrderRepository repoOrder, IOrderRoomRepository repoRoomOrder)
        {
            _repoOrder = repoOrder;
            _repoRoomOrder = repoRoomOrder;
        }

        public IQueryable<Order> ReadOrderByUserIDAsync(int accId)
        {
            if (accId < 1)
                throw new ArgumentException("Bad Request");

            var orderIds = _repoOrder.GetOrdersAsync(accId).OrderByDescending(s => s.Id);
           
            return orderIds;
        }
    }
}
