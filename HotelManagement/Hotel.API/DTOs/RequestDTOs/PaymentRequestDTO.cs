namespace Hotel.API.DTOs.RequestDTOs
{
    public class PaymentRequestDTO
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public decimal CostIncurred { get; set; }
        public decimal TotalMoneyInOrder {get; set; }
    }
}
