using Hotel.API.DTOs.RequestDTOs.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace Hotel.API.DTOs.RequestDTOs
{
    public class CommentCreateRequestDTO
    {
        [IdValidationAttribute]
        public int AccountId { get; set; }
        [IdValidationAttribute]
        public int RoomId { get; set; }
        [MinLength(1)]
        public string Content { get; set; } = null!;
        [Required]
        public bool Incognito { get; set; }
        public int ParentId { get; set; }
    }
}
