using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Rooms.DomainServices.Interfaces
{
    public interface ICreateCommentService
    {
        Task<bool> CheckPermission(int roomId, int userId);
        Task CreateCommentAsync(int accId, int roomId, string content, bool incoqnito, int parentId);
    }
}
