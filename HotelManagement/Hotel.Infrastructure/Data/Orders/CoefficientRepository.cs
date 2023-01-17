using Hotel.Domain.Orders.Entities;
using Hotel.Domain.Orders.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data.Orders
{
    public class CoefficientRepository : RepositoryBase<Coefficient>, ICoefficientRepository
    {
        public CoefficientRepository(HotelManagementContext context) : base(context)
        {

        }

        public Task AddEntityAsync(Coefficient entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Coefficient> GetEntityByIDAsync(int id)
        {
            var coef = DbSet.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(coef);
        }

        public IQueryable<Coefficient> GetEntityByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEntityAsync(Coefficient req)
        {
            throw new NotImplementedException();
        }
    }
}
