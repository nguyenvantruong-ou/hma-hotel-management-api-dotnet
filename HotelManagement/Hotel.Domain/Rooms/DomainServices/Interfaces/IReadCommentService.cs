using Hotel.Domain.Rooms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Rooms.DomainServices.Interfaces
{
    public interface IReadCommentService
    {
        Task<List<Comment>> ReadCommentAsync(int roomId, int toIndex);
        Task<int> CountCommentAsync(int roomId);
    }
}
