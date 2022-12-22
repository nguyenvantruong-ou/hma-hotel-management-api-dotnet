using System.ComponentModel.DataAnnotations;

namespace Hotel.API.DTOs.RequestDTOs
{
    public class AccountUpdateRequestDTO : AccountRequestDTO
    {
        public AccountUpdateRequestDTO() { }
        [Required]
        public int AccountId { get; set; }
    }
}
