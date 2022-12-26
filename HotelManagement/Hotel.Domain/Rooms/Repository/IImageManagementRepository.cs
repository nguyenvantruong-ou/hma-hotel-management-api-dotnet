using Hotel.Domain.Rooms.Entity;
using NET.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Rooms.Repository
{
    public interface IImageManagementRepository : IRepository<Image>
    {
        Task AddListImageAsync(List<string> ListImage, int RoomId);
        Task DeleteListImageAsync(int RoomId);
    }
}
