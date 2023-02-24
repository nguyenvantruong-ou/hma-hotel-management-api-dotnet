using Hotel.Domain.Rooms.Entities;
using Hotel.Domain.Rooms.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data.Rooms
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(HotelManagementContext context) : base(context)
        {

        }

        public async Task AddEntityAsync(Comment entity)
        {
            DbSet.Add(entity);
        }

        public async Task DeleteEntityAsync(int id)
        {
            var comment = DbSet.FirstOrDefault(s => s.Id == id);
            DbSet.Remove(comment!);
        }

        public Task<int> GetAmountCommentedAsync(int roomId, int userId)
        {
            var count = DbSet.Where(s => s.RoomId == roomId && s.AccountId == userId).Count();
            return Task.FromResult(count);
        }

        public Task<Comment> GetEntityByIDAsync(int id)
        {
            var comment = DbSet.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(comment!);
        }

        public IQueryable<Comment> GetEntityByName(string name)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Comment> ReadComments(int roomId)
        {
            var result = DbSet.Include(s => s.Account).Where(s => s.RoomId == roomId);
            return result;
        }

        public Task UpdateEntityAsync(Comment req)
        {
            throw new NotImplementedException();
        }
    }
}
