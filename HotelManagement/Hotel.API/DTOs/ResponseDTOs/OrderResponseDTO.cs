using Hotel.Domain.Orders.Entities;

namespace Hotel.API.DTOs.ResponseDTOs
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string TypeOrder { get; set; }
        public string Amount { get; set; }
        public bool? Status { get; set; }
        public bool? IsPay { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        public OrderResponseDTO(Order order)
        {
            Id = order.Id;
            UserName = order.Account.LastName + " " + order.Account.FirstName;
            PhoneNumber = order.Account.PhoneNumber;
            TypeOrder = order.Coefficient.Name;
            Amount = order.Capita.AmountOfPeople;
            Status = order.Status;
            IsPay = order.IsPay;
            DateCreated = order.DateCreated;
            StartDate = order.StartDate;
            EndDate = order.EndDate;
        }
    }
}
