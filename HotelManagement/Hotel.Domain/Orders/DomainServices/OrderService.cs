using Hotel.Domain.Orders.DomainServices.Interfaces;
using Hotel.Domain.Orders.Entities;
using Hotel.Domain.Orders.Repositories;
using Hotel.Domain.Rooms.Entities;
using Hotel.Domain.Rooms.Repositories;
using Hotel.Domain.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Orders.DomainServices
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _repoOrder;
        private IOrderRoomRepository _repoOrderRoom;
        private IRoomRepository _repoRoom;
        private IServiceRepository _repoService;
        private IOrderServiceRepository _repoOrderService;
        private ICoefficientRepository _repoCoefficient;
        private ICapitaRepository _repoCapita;

        public OrderService(IOrderRepository repo,
                            IOrderRoomRepository repoOrderRoom,
                            IRoomRepository repoRoom,
                            IServiceRepository repoService,
                            IOrderServiceRepository repoOrderService,
                            ICoefficientRepository repoCoefficient,
                            ICapitaRepository repoCapita)
        {
            _repoOrder = repo;
            _repoOrderRoom = repoOrderRoom;
            _repoRoom = repoRoom;
            _repoService = repoService;
            _repoOrderService = repoOrderService;
            _repoCoefficient = repoCoefficient;
            _repoCapita = repoCapita;
        }

        public async Task<decimal> GetTotalMoney(int capitaId, DateTime fromDate, DateTime toDate, List<int> roomIds, List<int> serviceIds)
        {
            if (capitaId < 1 || (roomIds.Count == 0 && serviceIds.Count ==0))
                throw new ArgumentException("Bad Request"); ;
            decimal totalMoney = 0;

            // room
            roomIds.ForEach(async s =>
            {
                var price = (await _repoRoom.GetEntityByIDAsync(s)).Price;
                TimeSpan date = (TimeSpan)(toDate - fromDate);
                decimal coef;
                if (date.Days <= 30)
                    coef = 1;
                else
                    coef = (decimal)(await _repoCoefficient.GetEntityByIDAsync(2)).Value;
                totalMoney += (price * coef + price * (decimal)(await _repoCapita.GetEntityByIDAsync(capitaId)).Value) * date.Days;

            });

            // service
            serviceIds.ForEach(async s =>
            {
                totalMoney += (await _repoService.GetEntityByIDAsync(s)).Price;
            });

            return totalMoney;
        }

        public async Task CreateOrderAsync(int accId, int capitaId, DateTime startDate, DateTime endDate)
        {
            if (accId < 1 || capitaId < 1)
                throw new ArgumentException("Bad Request");
            Order order = new Order();
            order.AccountId = accId;
            order.CapitaId = capitaId;
            order.Status = true;
            order.StartDate = startDate;
            order.EndDate = endDate;
            order.IsPay = false;

            TimeSpan date = (TimeSpan)(endDate - startDate);
            order.CoefficientId = date.Days > 30 ? 2 : 1;
            await _repoOrder.AddEntityAsync(order);
        }

        public async Task CreateOrderRoomAndServiceAsync(int accId, List<int> roomIds, List<int> serviceIds)
        {
            var order = await _repoOrder.GetOrderLatestAsync(accId);
            roomIds.ForEach(async s =>
            {
                // order room 
                OrderRoom room = new OrderRoom();
                room.RoomId = s;
                room.OrderId = order.Id;

                var price = (await _repoRoom.GetEntityByIDAsync(s)).Price;
                TimeSpan date =(TimeSpan) (order.EndDate - order.StartDate);

                decimal coef;
                if (date.Days <= 30)
                    coef = 1;
                else 
                    coef  = (decimal)order.Coefficient.Value;
                room.Price =  (price * coef + price * (decimal)order.Capita.Value) * date.Days;

                await _repoOrderRoom.AddEntityAsync(room);

            });

            serviceIds.ForEach(async s =>
            {
                // order service
                Hotel.Domain.Orders.Entities.OrderService service = new Hotel.Domain.Orders.Entities.OrderService();
                service.ServiceId = s;
                service.OrderId = order.Id;
                service.Price = (await _repoService.GetEntityByIDAsync(s)).Price;
                await _repoOrderService.AddEntityAsync(service);
            });
        }


        public async Task DeleteOrderAsync(int orderId)
        {
            if (orderId < 1)
                throw new ArgumentException("Bad Request");
            await _repoOrder.DeleteEntityAsync(orderId);
        }


        public async Task<List<Order>> ReadOrdersAsync(string phoneNumber)
        {
            var result = _repoOrder.GetEntityByName("")
                .Where(s => string.IsNullOrEmpty(phoneNumber) || s.Account.PhoneNumber.Contains(phoneNumber)).ToList();

            return result;
        }

        public async Task<List<Room>> ReadRoomsByDateAsync(DateTime fromDate, DateTime toDate, int capId)
        {
            if (fromDate > toDate)
            throw new ArgumentException("Kiểm tra lại ngày đặt phòng!");

            var results = _repoRoom.GetRooms(fromDate, toDate).ToList();
            TimeSpan date = (TimeSpan)(toDate - fromDate);
            decimal coef;
            results.ForEach(async s =>
            {
                if (date.Days <= 30)
                    coef = 1;
                else
                    coef = (decimal)(await _repoCoefficient.GetEntityByIDAsync(2)).Value;
                s.Price = (s.Price * coef + s.Price * (decimal)(await _repoCapita.GetEntityByIDAsync(capId)).Value) * (date.Days > 0 ? date.Days : 1);
            });
            return results;
        }

        public Task<List<Order>> ReadOrdersByStaff(string phoneNumber)
        {
            var results = _repoOrder.GetOrdersbyStaff().Where(s => String.IsNullOrEmpty(phoneNumber) || s.Account.PhoneNumber.Contains(phoneNumber)).ToList();
            return Task.FromResult(results);
        }

        public IQueryable<Order> ReadOrdersByStaff()
        {
            return _repoOrder.GetOrdersbyStaff();
        }
    }
}
