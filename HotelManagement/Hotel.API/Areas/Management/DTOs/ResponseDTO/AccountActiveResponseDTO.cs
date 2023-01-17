using Hotel.Domain.Accounts.Entities;

namespace Hotel.API.Areas.Management.DTOs.ResponseDTO
{
    public class AccountActiveResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public string? Gender { get; set; }
        public string CardId { get; set; } = null!;
        public string Address { get; set; } = null!;
        public AccountActiveResponseDTO(Account account)
        {
            Id = account.Id;
            Email = account.Email;
            FirstName = account.FirstName;
            LastName = account.LastName;
            PhoneNumber = account.PhoneNumber;
            Avatar = account.Avatar;
            Gender = account.Gender;
            CardId = account.CardId;
            Address = account.Address;
        }
    }
}
