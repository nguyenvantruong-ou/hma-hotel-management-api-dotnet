using Hotel.Domain.Orders.Entities;
using Hotel.Domain.Orders.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data.Orders
{
    public class BillRepository : RepositoryBase<Bill>, IBillRepository
    {
        public BillRepository(HotelManagementContext context) : base(context)
        {

        }

        public async Task AddEntityAsync(Bill entity)
        {
            DbSet.Add(entity);
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Bill> GetBillById(int id)
        {
            var result = DbSet.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(result);
        }

        public Task<Bill> GetEntityByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Bill> GetEntityByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEntityAsync(Bill req)
        {
            throw new NotImplementedException();
        }
    }
}
