using Hotel.API.DTOs.RequestDTOs.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace Hotel.API.DTOs.RequestDTOs
{
    public class CommentUpdateRequestDTO
    {
        [IdValidationAttribute]
        public int Id { get; set; }
        [MinLength(1)]
        public string Content { get; set; } = null!;
        public bool Incognito { get; set; }
    }
}
