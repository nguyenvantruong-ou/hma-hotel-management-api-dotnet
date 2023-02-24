using Hotel.API.DTOs.RequestDTOs.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace Hotel.API.DTOs.RequestDTOs
{
    public class CommentReadRequestDTO
    {
        [IdValidationAttribute]
        public int RoomId { get; set; }
        [Required]
        public int ToIndex { get; set; }
    }
}
