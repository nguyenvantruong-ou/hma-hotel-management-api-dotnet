using Hotel.Domain.Orders.Entities;
using Hotel.Domain.Orders.Repositories;
using Hotel.Domain.Services.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data.Orders
{
    public class OrderServiceRepository : RepositoryBase<OrderService>, IOrderServiceRepository
    {
        public OrderServiceRepository(HotelManagementContext context) : base(context)
        {

        }

        public async Task AddEntityAsync(OrderService entity)
        {
            DbSet.Add(entity);
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderService> GetEntityByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<OrderService> GetEntityByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Service>> ReadServicesHistoryAsync(int orderId)
        {
            var services = DbSet.Include(s => s.Service).Where(s => s.OrderId == orderId).Select(s => s.Service).ToList();
            return services;
        }

        public Task UpdateEntityAsync(OrderService req)
        {
            throw new NotImplementedException();
        }
    }
}
