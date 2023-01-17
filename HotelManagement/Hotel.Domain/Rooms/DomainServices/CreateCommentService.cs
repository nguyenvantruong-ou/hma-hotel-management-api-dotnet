using Hotel.Domain.Orders.Repositories;
using Hotel.Domain.Rooms.DomainServices.Interfaces;
using Hotel.Domain.Rooms.Entities;
using Hotel.Domain.Rooms.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Rooms.DomainServices
{
    public class CreateCommentService : ICreateCommentService
    {
        private readonly IOrderRepository _repoOrder;
        private readonly ICommentRepository _repoComment;
        public CreateCommentService(IOrderRepository repoOrde, ICommentRepository repoComment)
        {
            _repoOrder = repoOrde;
            _repoComment = repoComment;
        }

        public async Task<bool> CheckPermission(int roomId, int userId)
        {
            var count = (await _repoOrder.GetAmountPaymentedAsync(roomId, userId)) -
                    (await _repoComment.GetAmountCommentedAsync(roomId, userId));
            return count > 0 ? true : false;
        }

        public async Task CreateCommentAsync(int accId, int roomId, string content, bool incoqnito, int parentId)
        {
            if (accId < 1 || roomId < 1 || content.Length == 0)
                throw new Exception("Bad Request");

            Comment comment = new Comment();
            comment.AccountId = accId;
            comment.RoomId = roomId;
            comment.Content = content;
            comment.Incognito = incoqnito;
            comment.ParentId = parentId;
            await _repoComment.AddEntityAsync(comment);
        }
    }
}
