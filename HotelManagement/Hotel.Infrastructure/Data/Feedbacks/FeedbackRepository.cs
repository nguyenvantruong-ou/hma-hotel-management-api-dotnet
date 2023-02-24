using Hotel.Domain.Feedbacks.Entities;
using Hotel.Domain.Feedbacks.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data.Feedbacks
{
    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(HotelManagementContext context) : base(context)
        {

        }

        public async Task AddEntityAsync(Feedback entity)
        {
            DbSet.Add(entity);
        }

        public Task<int> CountUnreadFeedbackAsync()
        {
            var amount = DbSet.Where(s => s.IsRead == false).Count();
            return Task.FromResult(amount);
        }

        public async Task DeleteEntityAsync(int id)
        {
            var Fb = DbSet.FirstOrDefault(f => f.Id == id);
            DbSet.Remove(Fb);
        }

        public Task<Feedback> GetEntityByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Feedback> GetEntityByName(string name)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Feedback> GetFeedbacks(int userId)
        {
            return DbSet.Where(s => s.AccountId == userId);
        }

        public IQueryable<Feedback> GetGeneralFeedbacks()
        {
            return DbSet.Include(s => s.Account);
        }

        public async Task<List<Feedback>> GetListFeedbackAsync(int id)
        {
            var Results = DbSet.Where(s => s.Id == id).ToList();
            return Results;
        }

        public Task UpdateEntityAsync(Feedback req)
        {
            throw new NotImplementedException();
        }
    }
}
