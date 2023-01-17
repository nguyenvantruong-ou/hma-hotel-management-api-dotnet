using Hotel.Domain.Orders.Repositories;
using Hotel.Domain.Rooms.DomainServices.Interfaces;
using Hotel.Domain.Rooms.Entities;
using Hotel.Domain.Rooms.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Rooms.DomainServices
{
    public class ReadRoomService : IReadRoomService
    {
        private readonly IRoomRepository _repoRoom;
        private readonly IOrderRoomRepository _repoOrderRoom;
        public ReadRoomService(IRoomRepository repoRoom, IOrderRoomRepository repoOrderRoom)
        {
            _repoRoom = repoRoom;
            _repoOrderRoom = repoOrderRoom;
        }

        public Task<int> GetPageMaxAsync(string kw, int pageSize)
        {
            int Count = _repoRoom.GetEntityByName(kw).ToList().Count;
            int Max = Count / pageSize;
            return Task.FromResult(Count % pageSize == 0 ? Max : Max + 1);
        }

        public async Task<Room> ReadRoomAsync(int id)
        {
            if (id < 1)
                throw new InvalidDataException("Id must be a positive integer!");
            return await _repoRoom.GetEntityByIDAsync(id);
        }

        public Task<List<Room>> ReadRoomsAsync(string kw, int pageSize, int page, int sort)
        {
            var Result =_repoRoom
                .GetRooms(kw, sort)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .Select(_=> new
                {
                    Room= _,
                    Image = _.Images.FirstOrDefault(),
                }).ToList();

            return Task.FromResult(Result.Select(_ =>{
                _.Room.Images = new List<Image> { _.Image };
                return _.Room;
            }).ToList());
        }

        public async Task<List<Room>> ReadRoomsHistoryAsync(int orderId)
        {
            var result = (await _repoOrderRoom.ReadRoomsHistoryAsync(orderId)).Select(_ => new
            {
                Room = _,
                Image = _.Images.FirstOrDefault(),
            }).ToList(); ;

            return result.Select(_ => {
                _.Room.Images = new List<Image> { _.Image };
                return _.Room;
            }).ToList();
        }
    }
}
