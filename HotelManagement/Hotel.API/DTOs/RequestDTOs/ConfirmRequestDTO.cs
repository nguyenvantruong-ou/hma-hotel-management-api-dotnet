using System.ComponentModel.DataAnnotations;

namespace Hotel.API.DTOs.RequestDTOs
{
    public class ConfirmRequestDTO
    {
        [Required]
        public int Code { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
