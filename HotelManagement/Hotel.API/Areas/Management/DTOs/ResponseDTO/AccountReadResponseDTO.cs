using Hotel.Domain.Accounts.Entities;

namespace Hotel.API.Areas.Management.DTOs.ResponseDTO
{
    public class AccountReadResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Role { get; set; }
        public string? Gender { get; set; }
        public string CardId { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Address { get; set; }
        public bool? Status { get; set; }
        public string Avatar { get; set; }
        public DateTime? DateCreated { get; set; }
        public decimal Salary { get; set; } = 0;
        public string TypeStaff { get; set; } = null!;
        public int? StatusStaff { get; set; }

        public AccountReadResponseDTO() { }
        public AccountReadResponseDTO(Account account) {
            Id = account.Id;
            Email = account.Email;
            FirstName = account.FirstName;
            LastName = account.LastName;
            Role = account.Role.RoleName;
            Gender = account.Gender;
            CardId = account.CardId;
            PhoneNumber = account.PhoneNumber;
            Address = account.Address;
            Status = account.Status;
            DateCreated = account.DateCreated;
        }
    }
}
