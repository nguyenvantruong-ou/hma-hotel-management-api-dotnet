using Hotel.Domain.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Orders.DomainServices.Interfaces
{
    public interface IHistoryService
    {
        IQueryable<Order> ReadOrderByUserIDAsync(int accId);
    }
}
