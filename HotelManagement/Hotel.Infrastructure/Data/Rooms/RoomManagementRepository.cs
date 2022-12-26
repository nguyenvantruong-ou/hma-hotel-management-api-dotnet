using Hotel.Domain.Rooms.Entity;
using Hotel.Domain.Rooms.Repository;
using NET.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data.Rooms
{
    public class RoomManagementRepository : RepositoryBase<Room>, IRoomManagementRepository
    {
        public RoomManagementRepository(HotelManagementContext context) : base(context)
        {

        }

        public async Task AddEntityAsync(Room Entity)
        {
            DbSet.Add(Entity);
        }

        public async Task DeleteEntityAsync(int Id)
        {
            var Room = DbSet.FirstOrDefault(s => s.Id == Id);
            Room.Status = false;
        }

        public async Task<Room> GetEntityByIDAsync(int Id)
        {
            return DbSet.FirstOrDefault(s => s.Id == Id);
        }

        public IQueryable<Room> GetEntityByName(string Name)
        {
            return from room in DbSet
                   where string.IsNullOrEmpty(Name) || room.RoomName.Contains(Name)
                   select room;
        }

        public async Task<int> GetRoomIdLatestAsync()
        {
            return (from room in DbSet
                    orderby room.Id descending 
                    select room.Id).FirstOrDefault();
        }

        public async Task<Room> GetRoomBySlugAsync(string Slug)
        {
            return DbSet.FirstOrDefault(s => s.Slug.Equals(Slug));
        }

        public async Task<bool> IsExistSlug(string Slug)
        {
            return DbSet.FirstOrDefault(s => s.Slug.Equals(Slug)) != null ? true : false;
        }

        public async Task UpdateEntityAsync(Room Req)
        {
            var Room = DbSet.FirstOrDefault(s => s.Id == Req.Id);
            Room.Slug = Req.Slug;
            Room.RoomName = Req.RoomName;
            Room.Price = Req.Price;
            Room.Description = Req.Description;
            Room.BedType = Req.BedType;
            Room.Acreage = Req.Acreage;
        }

        public async Task<bool> IsExistNameAsync(string RoomName)
        {
            return DbSet.FirstOrDefault(s => s.RoomName.Equals(RoomName)) != null ? true : false;
        }

        public async Task<bool> IsExistNameByIdAsync(string RoomName, int Id)
        {
            return DbSet.FirstOrDefault(s => s.RoomName.Equals(RoomName) && s.Id != Id) != null ? true : false;
        }
    }
}
