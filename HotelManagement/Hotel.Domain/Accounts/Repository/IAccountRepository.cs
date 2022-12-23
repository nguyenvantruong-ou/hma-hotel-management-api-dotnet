﻿using Hotel.Domain.Accounts.Entity;
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
        Task<bool> IsEmailExistAsync(string Email);
        Task<bool> IsCardIdExistAsync(string CardId);
        Task AccountActivatedAsync(string Email);
        Task<Account> SignInAsync(string Email, string Password);
    }
}
