using Hotel.Domain.Rooms.Entities;

namespace Hotel.API.DTOs.ResponseDTOs
{
    public class RoomDetailResponseDTO
    {
        public string RoomName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? BedType { get; set; }
        public string Acreage { get; set; } = null!;
        public List<string?> ListImages { get; set; }

        public RoomDetailResponseDTO()
        {
        }
        public RoomDetailResponseDTO(Room room)
        {
            RoomName = room.RoomName;
            Price = room.Price;
            Description = room.Description;
            BedType = room.BedType;
            Acreage = room.Acreage;
            ListImages = room.Images.Select(s => s.Link).ToList();
        }

       
    }
}
