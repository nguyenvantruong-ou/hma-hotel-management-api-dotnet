﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Statistics
{
    public interface IStatisticalServiceRepository
    {
        Task<Decimal> GetRevenueServiceAsync(int month, int year);
    }
}
