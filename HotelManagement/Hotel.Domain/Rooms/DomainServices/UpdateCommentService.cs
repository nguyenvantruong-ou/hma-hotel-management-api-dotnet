using Hotel.Domain.Rooms.DomainServices.Interfaces;
using Hotel.Domain.Rooms.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Rooms.DomainServices
{
    public class UpdateCommentService : IUpdateCommentService
    {
        private readonly ICommentRepository _repoComment;
        public UpdateCommentService(ICommentRepository repoComment)
        {
            _repoComment = repoComment;
        }

        public async Task UpdateCommentAsync(int id, string content, bool isIncognito)
        {
            if(id < 0 || String.IsNullOrEmpty(content))
                throw new ArgumentNullException("Bad Request!");

            var comment = await _repoComment.GetEntityByIDAsync(id);

            if (comment == null)
                throw new ArgumentException("No results were found!");

            comment.Content = content;
            comment.Incognito = isIncognito;
        }
    }
}
