using Hotel.Domain.Orders.Entities;
using Hotel.Domain.Orders.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data.Orders
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(HotelManagementContext context) : base(context)
        {

        }

        public async Task AddEntityAsync(Order entity)
        {
            DbSet.Add(entity);
        }

        public async Task DeleteEntityAsync(int id)
        {
            var order = DbSet.FirstOrDefault(s => s.Id == id);
            order.Status = false;
        }

        public async Task<Order> GetEntityByIDAsync(int id)
        {
            return DbSet.FirstOrDefault(s => s.Id == id);
        }

        public IQueryable<Order> GetEntityByName(string name)
        {
            var results = DbSet.Include(s => s.Coefficient).Include(s => s.Account).Include(s => s.Capita);
            return results;
        }

        public Task<Order> GetOrderLatestAsync(int accId)
        {
            var order = DbSet.Include(s => s.Coefficient).Include(s => s.Capita).OrderByDescending(s => s.Id).FirstOrDefault(s => s.AccountId == accId);
            return Task.FromResult(order);
        }

        public IQueryable<Order> GetOrdersAsync(int accId)
        {
            var results = DbSet.Include(s => s.Account).Include(s => s.Capita).Where(s => s.AccountId == accId);
            return results;
        }

        public Task<int> GetAmountPaymentedAsync(int roomId, int userId)
        {
            var count = DbSet.Where(s => s.IsPay == true && s.AccountId == userId && s.OrderRooms.Any(s => s.RoomId == roomId)).Count();
            return Task.FromResult(count);
        }

        public Task UpdateEntityAsync(Order req)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> GetOrdersbyStaff()
        {
            var results = DbSet.Include(s => s.Account).Include(s => s.Capita)
                .OrderByDescending(s => s.DateCreated);
            return results;
        }
    }
}
