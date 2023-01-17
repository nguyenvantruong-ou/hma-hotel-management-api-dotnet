using Hotel.Domain.Orders.Entities;
using Hotel.Domain.Rooms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Orders.DomainServices.Interfaces
{
    public interface IOrderService
    {
        Task<decimal> GetTotalMoney(int capId, DateTime fromDate, DateTime toDate, List<int> roomIds, List<int> serviceIds);
        Task CreateOrderAsync(int accId, int capitaId, DateTime startDate, DateTime endDate);
        Task CreateOrderRoomAndServiceAsync(int accId, List<int> roomIds, List<int> serviceIds);
        Task DeleteOrderAsync(int orderId);
        Task<List<Order>> ReadOrdersAsync(string phoneNumber);
        Task<List<Room>> ReadRoomsByDateAsync(DateTime fromDate, DateTime toDate, int capId);
        IQueryable<Order> ReadOrdersByStaff();
    }
}
