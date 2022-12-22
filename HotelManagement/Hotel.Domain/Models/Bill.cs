using Hotel.Domain.Accounts.Entity;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Models
{
    public partial class Bill
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? StaffId { get; set; }
        public decimal? CostsIncurred { get; set; }
        public decimal TotalMoney { get; set; }

        public virtual Order IdNavigation { get; set; } = null!;
        public virtual Account? Staff { get; set; }
    }
}
