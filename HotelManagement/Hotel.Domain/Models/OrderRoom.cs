using System;
using System.Collections.Generic;

namespace Hotel.Domain.Models
{
    public partial class OrderRoom
    {
        public int Id { get; set; }
        public int? RoomId { get; set; }
        public int? OrderId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Room? Room { get; set; }
    }
}
