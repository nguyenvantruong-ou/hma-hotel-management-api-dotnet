using Hotel.API.DTOs.RequestDTOs.CustomValidationAttribute;

namespace Hotel.API.DTOs.RequestDTOs
{
    public class ReadRoomOrderRequestDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [IdValidationAttribute]
        public int CapitaId { get; set; }
    }
}
