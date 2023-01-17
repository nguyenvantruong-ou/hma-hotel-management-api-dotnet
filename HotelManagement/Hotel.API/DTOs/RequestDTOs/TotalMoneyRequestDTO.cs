namespace Hotel.API.DTOs.RequestDTOs
{
    public class TotalMoneyRequestDTO
    {
        public int CapitaId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // order room
        public List<int> RoomId { get; set; }

        // order service
        public List<int> ServiceId { get; set; }
    }
}
