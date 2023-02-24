using Hotel.Domain.Accounts.Constant;
using Hotel.Domain.Accounts.DomainServices.Interfaces;
using Hotel.Domain.Accounts.Entities;

namespace Hotel.API.DomainServices
{
    public class ConvertToAccountService : IConvertToAccountService
    {
        public Account ConvertAccount(string email, string password, string firstName, string lastName, 
                                        int gender, string cardId, string phoneNumber, string address)
        {
            var acc = new Account();
            acc.Email = email;
            acc.Password = password;
            acc.FirstName = firstName;
            acc.LastName = lastName;
            acc.Gender = GetGender(gender);
            acc.CardId = cardId;
            acc.PhoneNumber = phoneNumber;
            acc.Address = address;

            return acc;
        }

        public string GetGender(int type)
        {
            if (type == 1)
                return Gender.Male;
            return type == 2 ? Gender.Female : Gender.Other;
        }
    }
}
