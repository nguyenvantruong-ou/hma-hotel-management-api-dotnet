using System.ComponentModel.DataAnnotations;

namespace Hotel.API.DTOs.RequestDTOs
{
    public class EmailRequestDTO
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
