using Hotel.Domain.Orders.Entities;
using Hotel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Orders.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOrderLatestAsync(int accId);
        IQueryable<Order> GetOrdersAsync(int accId);
        Task<int> GetAmountPaymentedAsync(int roomId, int userId);
        IQueryable<Order> GetOrdersbyStaff();
    }
}
