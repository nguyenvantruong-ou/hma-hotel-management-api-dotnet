using Hotel.Domain.Accounts.Entity;
using NET.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Accounts.Repository
{
    public interface ITokenRegisterRepository : IRepository<TokenRegister>
    {
        Task DeleteAsync(string Email);
        Task<TokenRegister> GetTokenAsync(string Email);
    }
}
