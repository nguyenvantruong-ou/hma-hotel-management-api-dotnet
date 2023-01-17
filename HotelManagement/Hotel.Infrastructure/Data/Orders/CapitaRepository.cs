using Hotel.Domain.Orders.Entities;
using Hotel.Domain.Orders.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data.Orders
{
    public class CapitaRepository : RepositoryBase<Capita>, ICapitaRepository
    {
        public CapitaRepository(HotelManagementContext context) : base(context)
        {

        }

        public Task AddEntityAsync(Capita entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Capita> GetEntityByIDAsync(int id)
        {
            var capita = DbSet.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(capita);
        }

        public IQueryable<Capita> GetEntityByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEntityAsync(Capita req)
        {
            throw new NotImplementedException();
        }
    }
}
