using Hotel.Domain.Interfaces;
using Hotel.Domain.Orders.Entities;
using Hotel.Domain.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Orders.Repositories
{
    public interface IOrderServiceRepository : IRepository<OrderService>
    {
        Task<List<Service>> ReadServicesHistoryAsync(int orderId);
    }
}
