using Hotel.Domain.Services.Entities;
using Hotel.Domain.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data.Services
{
    public class ServiceRepository : RepositoryBase<Service>, IServiceRepository
    {
        public ServiceRepository(HotelManagementContext context) : base(context)
        {

        }

        public Task AddEntityAsync(Service entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Service> GetEntityByIDAsync(int id)
        {
            var service = DbSet.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(service);
        }

        public IQueryable<Service> GetEntityByName(string name)
        {
            return from service in DbSet
                   where service.Status == true
                   select service;
        }

        public Task UpdateEntityAsync(Service req)
        {
            throw new NotImplementedException();
        }
    }
}
