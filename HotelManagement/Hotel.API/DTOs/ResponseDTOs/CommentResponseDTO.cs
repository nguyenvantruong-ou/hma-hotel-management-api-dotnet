using Hotel.Domain.Rooms.Entities;

namespace Hotel.API.DTOs.ResponseDTOs
{
    public class CommentResponseDTO
    {
        public int AmountComment { get; set; }
        public List<CommentInfoResponseDTO> ListComment { get; set; }
        public CommentResponseDTO()
        {
            ListComment = new List<CommentInfoResponseDTO>();
        }
    }
}
