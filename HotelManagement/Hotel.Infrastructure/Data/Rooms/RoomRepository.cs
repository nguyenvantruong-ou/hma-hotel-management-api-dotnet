using Hotel.Domain.Rooms.Entities;
using Hotel.Domain.Rooms.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data.Rooms
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(HotelManagementContext context) : base(context)
        {

        }

        public Task AddEntityAsync(Room entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Room> GetEntityByIDAsync(int id)
        {
            var result = DbSet.Include(s => s.Images).Include(s => s.Comments).FirstOrDefault(s => s.Id == id && s.Status == true);
            return Task.FromResult(result);
        }

        public IQueryable<Room> GetEntityByName(string name)
        {
            return DbSet.Where(s => (string.IsNullOrEmpty(name) || s.RoomName.Contains(name)) && s.Status == true);
        }

        public Task<int> GetPageMaxAsync(string kw, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Room> GetRooms(string name, int sort)
        {
            var rooms = DbSet.Include(s => s.Images);
            if (sort == 0)
                return from room in rooms
                       where (string.IsNullOrEmpty(name) || room.RoomName.Contains(name)) && room.Status == true
                       select room;
            if (sort == -1)
                return from room in rooms
                       where (string.IsNullOrEmpty(name) || room.RoomName.Contains(name)) && room.Status == true
                       orderby room.Price descending
                       select room;
            return from room in rooms
                   where (string.IsNullOrEmpty(name) || room.RoomName.Contains(name)) && room.Status == true
                   orderby room.Price ascending
                   select room;
        }

        public IQueryable<Room> GetRooms(DateTime startDate, DateTime endDate)
        {
            var ordered = DbSet.Include(s => s.Images).Where(s => s.Status == true && 
                        (s.OrderRooms.Any(s =>  (startDate <= s.Order.StartDate && endDate >= s.Order.StartDate) || 
                                                (startDate <= s.Order.EndDate && endDate >= s.Order.EndDate)) ));
            var results = DbSet.Include(s => s.Images).Where(s => s.Status == true).Except(ordered);
            return results;
        }

        public Task UpdateEntityAsync(Room req)
        {
            throw new NotImplementedException();
        }
    }
}
