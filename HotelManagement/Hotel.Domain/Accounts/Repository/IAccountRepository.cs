using Hotel.Domain.Accounts.Entity;
using NET.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Accounts.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<bool> IsEmailExist(string Email);
        Task<bool> IsCardIdExist(string CardId);
        Task AccountActivated(string Email);
    }
}
