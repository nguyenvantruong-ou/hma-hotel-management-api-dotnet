using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetEntityByIDAsync(int Id);
        IQueryable<TEntity> GetEntityByName(string Name);
        Task AddEntityAsync(TEntity Entity);
        Task UpdateEntityAsync(TEntity Req);
        Task DeleteEntityAsync(int Id);
    }
}
