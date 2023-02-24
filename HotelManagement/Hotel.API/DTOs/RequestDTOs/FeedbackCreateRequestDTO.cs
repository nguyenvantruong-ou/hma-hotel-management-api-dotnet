using Hotel.API.DTOs.RequestDTOs.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace Hotel.API.DTOs.RequestDTOs
{
    public class FeedbackCreateRequestDTO
    {
        [MinLength(1)]
        public string Content { get; set; }
        [IdValidationAttribute]
        public int UserId { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
    }
}
