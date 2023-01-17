using Hotel.Domain.Orders.Entities;
using Hotel.Domain.Orders.Repositories;
using Hotel.Domain.Rooms.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data.Orders
{
    public class OrderRoomRepository : RepositoryBase<OrderRoom>, IOrderRoomRepository
    {
        public OrderRoomRepository(HotelManagementContext context) : base(context)
        {

        }

        public async Task AddEntityAsync(OrderRoom entity)
        {
            DbSet.Add(entity);
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderRoom> GetEntityByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<OrderRoom> GetEntityByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Room> ReadRoomsByDateAsync(DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Room>> ReadRoomsHistoryAsync(int orderId)
        {
            var rooms = DbSet.Include(s => s.Room.Images).Where(s => s.OrderId == orderId).Select(s => s.Room).ToList();
            return rooms;
        }

        public Task UpdateEntityAsync(OrderRoom req)
        {
            throw new NotImplementedException();
        }

    }
}
