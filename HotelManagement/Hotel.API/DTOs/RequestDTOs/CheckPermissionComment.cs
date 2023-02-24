using Hotel.API.DTOs.RequestDTOs.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace Hotel.API.DTOs.RequestDTOs
{
    public class CheckPermissionComment
    {
        [Required]
        [IdValidationAttribute]
        public int RoomId { get; set; }
        [Required]
        [IdValidationAttribute]
        public int UserId { get; set; }
    }
}
