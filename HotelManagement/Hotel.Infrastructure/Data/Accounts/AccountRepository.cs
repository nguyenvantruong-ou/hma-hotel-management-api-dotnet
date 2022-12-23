using Hotel.Domain.Accounts.Entity;
using Hotel.Domain.Accounts.Repository;
using Microsoft.EntityFrameworkCore;
using NET.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data.Accounts
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(HotelManagementContext context) : base(context)
        {

        }

        public async Task AccountActivatedAsync(string Email)
        {
            var Acc = DbSet.FirstOrDefault(s => s.Email.Equals(Email));
            Acc.Status = true;
        }

        public async Task AddEntityAsync(Account Entity)
        {
            DbSet.Add(Entity);
        }

        public async Task DeleteEntityAsync(int Id)
        {
            var Acc = DbSet.FirstOrDefault(s => s.Id == Id);
            Acc.Status = false;

        }

        public Task<Account> GetEntityByIDAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Account> GetEntityByName(string Name)
        {
            return from account in DbSet
                   where string.IsNullOrEmpty(Name) || account.Email.Contains(Name) 
                   select account;
        }

        public async Task<bool> IsCardIdExistAsync(string CardId)
        {
            return DbSet.FirstOrDefault(s => s.CardId == CardId) != null ? true : false;
        }

        public async Task<bool> IsEmailExistAsync(string Email)
        {
            return DbSet.FirstOrDefault(s => s.Email == Email) != null ? true : false;
        }

        public async Task<Account> SignInAsync(string Email, string Password)
        {
            var result = DbSet.Include(s=> s.Role).FirstOrDefault(s => s.Email.Equals(Email) && s.Password.Equals(Password) && s.Status == true);

            return result;
        }

        public async Task UpdateEntityAsync(Account Req)
        {
            var Acc = DbSet.FirstOrDefault(s => s.Id == Req.Id);
            Acc.Email = Req.Email;
            Acc.Password = Req.Password;
            Acc.FirstName = Req.FirstName;
            Acc.LastName = Req.LastName;
            Acc.Role = Req.Role;
            Acc.Gender = Req.Gender;
            if (Req.Avatar.Length > 0)
                Acc.Avatar = Req.Avatar;
            Acc.CardId = Req.CardId;
            Acc.PhoneNumber = Req.PhoneNumber;
            Acc.Address = Req.Address;
        }
    }
}
