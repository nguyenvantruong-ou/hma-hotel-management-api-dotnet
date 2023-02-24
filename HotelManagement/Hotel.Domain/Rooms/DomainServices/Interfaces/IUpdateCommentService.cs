using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Rooms.DomainServices.Interfaces
{
    public interface IUpdateCommentService
    {
        Task UpdateCommentAsync(int id, string content, bool isIncognito);
    }
}
