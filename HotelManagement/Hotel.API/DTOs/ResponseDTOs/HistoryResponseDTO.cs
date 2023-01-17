using Hotel.Domain.Rooms.Entities;
using Hotel.Domain.Services.Entities;

namespace Hotel.API.DTOs.ResponseDTOs
{
    public class HistoryResponseDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string AmountOfPeople { get; set; }
        public bool? Status { get; set; }
        public bool? IsPay { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal TotalMoney { get; set; }
        public List<RoomsHomeResponse> ListRooms { get; set; }
        public List<Service> ListServices { get; set; }

        public HistoryResponseDTO(int id, string username, string amountOfPeople, bool? status, bool? isPay, decimal total,
                                  DateTime? dateCreated, DateTime? startDate, DateTime? endDate, List<RoomsHomeResponse> rooms, List<Service> services)
        {
            Id = id;
            Username = username;
            AmountOfPeople = amountOfPeople;
            Status = status;
            IsPay = isPay;
            TotalMoney = total;
            DateCreated = dateCreated;
            StartDate = startDate;
            EndDate = endDate;
            ListRooms = rooms;
            ListServices = services;
        }
    }
}
