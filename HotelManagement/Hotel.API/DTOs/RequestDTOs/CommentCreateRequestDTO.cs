namespace Hotel.API.DTOs.RequestDTOs
{
    public class CommentCreateRequestDTO
    {
        public int AccountId { get; set; }
        public int RoomId { get; set; }
        public string Content { get; set; } = null!;
        public bool Incognito { get; set; }
        public int ParentId { get; set; }
    }
}
