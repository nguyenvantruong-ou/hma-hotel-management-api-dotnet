using Hotel.API.DTOs.RequestDTOs.CustomValidationAttribute;

namespace Hotel.API.DTOs.RequestDTOs
{
    public class PaymentRequestDTO
    {
        [IdValidationAttribute]
        public int Id { get; set; }
        [IdValidationAttribute]
        public int StaffId { get; set; }
        public decimal CostIncurred { get; set; }
        public decimal TotalMoneyInOrder {get; set; }
    }
}
