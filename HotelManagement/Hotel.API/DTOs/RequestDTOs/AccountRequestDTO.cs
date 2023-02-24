using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hotel.API.DTOs.RequestDTOs
{
    public class AccountRequestDTO
    {
        public IFormFile? File { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Range(1,2)]
        public int Gender { get; set; }

        [Required]
        [MaxLength(12)]
        [MinLength(9)]
        public string CardId { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? Address { get; set; } = null;

    }
}
