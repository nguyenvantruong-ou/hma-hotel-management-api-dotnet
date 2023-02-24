using Hotel.Domain.Rooms.DomainServices.Interfaces;
using Hotel.Domain.Rooms.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Rooms.DomainServices
{
    public class DeleteCommentService : IDeleteCommentService
    {
        private readonly ICommentRepository _repoComment;
        public DeleteCommentService(ICommentRepository repoComment)
        {
            _repoComment = repoComment;
        }

        public async Task DeleteCommentAsync(int id)
        {
            if (id < 1)
                throw new Exception("Id must be a positive integer!");
            await _repoComment.DeleteEntityAsync(id);
        }
    }
}
