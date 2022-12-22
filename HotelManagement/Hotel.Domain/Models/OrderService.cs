using System;
using System.Collections.Generic;

namespace Hotel.Domain.Models
{
    public partial class OrderService
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public int? OrderId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Service? Service { get; set; }
    }
}
