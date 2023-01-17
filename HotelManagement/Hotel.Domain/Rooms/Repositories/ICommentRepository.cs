using Hotel.Domain.Interfaces;
using Hotel.Domain.Rooms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Rooms.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IQueryable<Comment> ReadComments(int roomId);
        Task<int> GetAmountCommentedAsync(int roomId, int userId);
    }
}
