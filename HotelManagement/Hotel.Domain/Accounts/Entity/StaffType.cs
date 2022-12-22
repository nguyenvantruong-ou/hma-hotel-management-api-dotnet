using System;
using System.Collections.Generic;

namespace Hotel.Domain.Accounts.Entity
{
    public partial class StaffType
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public bool? Status { get; set; }
    }
}
