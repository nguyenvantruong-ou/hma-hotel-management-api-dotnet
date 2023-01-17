using Hotel.Domain.Orders.DomainServices.Interfaces;
using Hotel.Domain.Orders.Entities;
using Hotel.Domain.Orders.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Orders.DomainServices
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _repoBill;
        private readonly IOrderRepository _repoOrder;
        public BillService(IBillRepository repoBill, IOrderRepository repoOrder)
        {
            _repoBill = repoBill;
            _repoOrder = repoOrder;
        }

        public async Task CreateBillAsync(int id, int staffId, decimal costIncurred, decimal totalMoneyInOrder)
        {
            if (id < 1 || staffId < 1 || totalMoneyInOrder < 1)
                throw new Exception("Bad Request");
            Bill bill = new Bill();
            bill.Id = id;
            bill.StaffId = staffId;
            bill.CostsIncurred = costIncurred;
            bill.TotalMoney = costIncurred + totalMoneyInOrder;
            await _repoBill.AddEntityAsync(bill);

            var order = await _repoOrder.GetEntityByIDAsync(id);
            order.IsPay = true;
        }

        public async Task<Bill> GetBillById(int id)
        {
            if (id < 1)
                throw new Exception("Id must be a positive integer!");
            return await _repoBill.GetBillById(id);
        }
    }
}
