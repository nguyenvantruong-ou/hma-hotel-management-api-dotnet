using System;
using System.Collections.Generic;

namespace Hotel.Domain.Accounts.Entity
{
    public partial class staff
    {
        public int Id { get; set; }
        public decimal Salary { get; set; }
        public int? TypeId { get; set; }
        public int? StatusStaff { get; set; }

        public virtual Account IdNavigation { get; set; } = null!;
        public virtual StaffType? Type { get; set; }
    }
}
