namespace Hotel.API.DTOs.RequestDTOs
{
    public class OrderReadByStaffRequestDTO
    {
        public string? phoneNumber { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
