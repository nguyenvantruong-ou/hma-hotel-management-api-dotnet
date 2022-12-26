using Hotel.API.DTOs.Constant;
using Hotel.API.DTOs.RequestDTOs;
using Hotel.API.Interfaces.Services;
using Hotel.Domain.Accounts.Entity;

namespace Hotel.API.Services
{
    public class AccountService : IAccountService
    {
        public Account ConvertAccount(AccountRequestDTO Req)
        {
            var Acc = new Account();
            Acc.Email = Req.Email;
            Acc.Password = Req.Password;
            Acc.FirstName = Req.FirstName;
            Acc.LastName = Req.LastName;
            Acc.Gender = GetGender(Req.Gender);
            Acc.CardId = Req.CardId;
            Acc.PhoneNumber = Req.PhoneNumber;
            Acc.Address = Req.Address;

            return Acc;
        }

        public string GetGender(int type)
        {
            if (type == 1)
                return Gender.Male;
            return type == 2 ? Gender.Female : Gender.Other;
        }
    }
}
