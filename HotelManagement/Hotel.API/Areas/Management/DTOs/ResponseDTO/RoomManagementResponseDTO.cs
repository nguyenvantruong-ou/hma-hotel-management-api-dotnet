using Hotel.Domain.Rooms.Entities;

namespace Hotel.API.Areas.Management.DTOs.ResponseDTO
{
    public class RoomManagementResponseDTO
    {
        public int Id { get; set; }
        public string Slug { get; set; } = null!;
        public string RoomName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? BedType { get; set; }
        public string Acreage { get; set; } = null!;
        public bool? Status { get; set; }

        public RoomManagementResponseDTO(Room room)
        {
            Id = room.Id;
            Slug = room.Slug;
            RoomName = room.RoomName;
            Price = room.Price;
            Description = room.Description;
            BedType = room.BedType;
            Acreage = room.Acreage;
            Status = room.Status;
        }
    }
}
