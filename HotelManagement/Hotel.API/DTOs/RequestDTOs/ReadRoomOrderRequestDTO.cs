namespace Hotel.API.DTOs.RequestDTOs
{
    public class ReadRoomOrderRequestDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CapitaId { get; set; }
    }
}
