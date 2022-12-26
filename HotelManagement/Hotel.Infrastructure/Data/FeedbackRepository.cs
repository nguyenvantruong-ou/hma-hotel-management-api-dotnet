using Hotel.Domain.Feedbacks.Entity;
using Hotel.Domain.Feedbacks.Repository;
using NET.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data
{
    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(HotelManagementContext context) : base(context)
        {

        }

        public async Task AddEntityAsync(Feedback Entity)
        {
            DbSet.Add(Entity);
        }

        public async Task DeleteEntityAsync(int Id)
        {
            var Fb = DbSet.FirstOrDefault(f => f.Id == Id);
            DbSet.Remove(Fb);
        }

        public Task<Feedback> GetEntityByIDAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Feedback> GetEntityByName(string Name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Feedback>> GetListFeedbackAsync(int Id)
        {
            var Results = DbSet.Where(s => s.Id == Id).ToList();
            return Results;
        }

        public Task UpdateEntityAsync(Feedback Req)
        {
            throw new NotImplementedException();
        }
    }
}
