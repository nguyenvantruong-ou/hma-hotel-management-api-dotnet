using Hotel.Domain.Orders.Entities;

namespace Hotel.API.DTOs.ResponseDTOs
{
    public class OrderReadByStaffResponseDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string AmountOfPeople { get; set; }
        public bool? Status { get; set; }
        public bool? IsPay { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        public OrderReadByStaffResponseDTO(Order order, decimal totalMoney)
        {
            Id = order.Id;
            Username = order.Account.LastName + " " + order.Account.FirstName;
            PhoneNumber = order.Account.PhoneNumber;
            AmountOfPeople = order.Capita.AmountOfPeople;
            Status = order.Status;
            IsPay = order.IsPay;
            DateCreated = order.DateCreated;
            StartDate = order.StartDate;
            EndDate = order.EndDate;
            TotalAmount = totalMoney;
        }
    }
}
