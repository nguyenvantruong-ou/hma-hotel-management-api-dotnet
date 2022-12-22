using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hotel.API.DTOs.RequestDTOs
{
    public class AccountRequestDTO
    {
        [ValidateNever]
        public IFormFile File { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Gender { get; set; }
        [MaxLength(12)]
        [MinLength(9)]
        public string CardId { get; set; }
        public string PhoneNumber { get; set; }
        [ValidateNever]
        public string? Address { get; set; } = null;

    }
}
