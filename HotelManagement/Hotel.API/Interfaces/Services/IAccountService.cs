using Hotel.API.DTOs.RequestDTOs;
using Hotel.Domain.Accounts.Entity;

namespace Hotel.API.Interfaces.Services
{
    public interface IAccountService
    {
        Account ConvertAccount(AccountRequestDTO Req);
        string GetGender(int type);

    }
}
