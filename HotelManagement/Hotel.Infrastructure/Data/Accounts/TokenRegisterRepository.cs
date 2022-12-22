using Hotel.Domain.Accounts.Entity;
using Hotel.Domain.Accounts.Repository;
using NET.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data.Accounts
{
    public class TokenRegisterRepository : RepositoryBase<TokenRegister>, ITokenRegisterRepository
    {
        public TokenRegisterRepository(HotelManagementContext context) : base(context)
        {

        }

        public async Task AddEntityAsync(TokenRegister Entity)
        {
            var Token = DbSet.FirstOrDefault(s => s.Email == Entity.Email);
            if (Token == null)
                DbSet.Add(Entity);
            else
            {
                Token.Token = Entity.Token;
                Token.Status = true;
            }
        }

        public async Task DeleteAsync(string Email)
        {
            var Token =  DbSet.FirstOrDefault(e => e.Email == Email);
            Token.Status = false;
        }

        public Task DeleteEntityAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<TokenRegister> GetEntityByIDAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TokenRegister> GetEntityByName(string Name)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenRegister> GetTokenAsync(string Email)
        {
            return DbSet.FirstOrDefault(s => s.Email.Equals(Email));
        }

        public Task UpdateEntityAsync(TokenRegister Req)
        {
            throw new NotImplementedException();
        }
    }
}
