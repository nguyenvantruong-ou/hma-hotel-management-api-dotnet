using Hotel.API.DTOs.RequestDTOs.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace Hotel.API.DTOs.RequestDTOs
{
    public class AccountUpdateRequestDTO : AccountRequestDTO
    {
        [Required]
        [IdValidationAttribute]
        public int AccountId { get; set; }
        public AccountUpdateRequestDTO() { }
    }
}
