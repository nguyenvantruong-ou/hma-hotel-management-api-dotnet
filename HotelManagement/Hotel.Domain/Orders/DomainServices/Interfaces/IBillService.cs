using Hotel.Domain.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Orders.DomainServices.Interfaces
{
    public interface IBillService
    {
        Task<Bill> GetBillById(int id);
        Task CreateBillAsync(int id, int staffId, decimal costIncurred, decimal totalMoneyInOrder);
    }
}
