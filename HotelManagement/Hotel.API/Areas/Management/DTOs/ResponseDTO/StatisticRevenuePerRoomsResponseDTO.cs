namespace Hotel.API.Areas.Management.DTOs.ResponseDTO
{
    public class StatisticRevenuePerRoomsResponseDTO
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public decimal TotalMoney { get; set; }

        public StatisticRevenuePerRoomsResponseDTO(int roomId, string roomName, decimal totalMoney)
        {
            RoomId = roomId;
            RoomName = roomName;
            TotalMoney = totalMoney;
        }
    }
}
