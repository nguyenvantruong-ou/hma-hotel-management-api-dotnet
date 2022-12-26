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
    public class ImageManagementRepository : RepositoryBase<Image>, IImageManagementRepository
    {
        public ImageManagementRepository(HotelManagementContext context) : base(context)
        {

        }

        public Task AddEntityAsync(Image Entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddListImageAsync(List<string> ListImage, int RoomId)
        {
            ListImage.ForEach(s =>
            {
                Image NewImage = new Image();
                NewImage.Link = s;
                NewImage.RoomId = RoomId;
                DbSet.Add(NewImage);
            });
        }

        public Task DeleteEntityAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteListImageAsync(int RoomId)
        {
            List<Image> ListImage = DbSet.Where(s => s.RoomId == RoomId).ToList();
            ListImage.ForEach(s => DbSet.Remove(s));
        }

        public Task<Image> GetEntityByIDAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Image> GetEntityByName(string Name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEntityAsync(Image Req)
        {
            throw new NotImplementedException();
        }
    }
}
