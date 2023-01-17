using Hotel.Domain.Interfaces;
using Hotel.Domain.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Services.Repositories
{
    public interface IServiceRepository : IRepository<Service>
    {
    }
}
