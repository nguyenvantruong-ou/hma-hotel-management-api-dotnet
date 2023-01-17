using Hotel.Domain.Interfaces;
using Hotel.Domain.Orders.Entities;
using Hotel.Domain.Rooms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Orders.Repositories
{
    public interface IOrderRoomRepository : IRepository<OrderRoom>
    {
        Task<Room> ReadRoomsByDateAsync(DateTime fromDate, DateTime toDate);
        Task<List<Room>> ReadRoomsHistoryAsync(int orderId);
    }
}
