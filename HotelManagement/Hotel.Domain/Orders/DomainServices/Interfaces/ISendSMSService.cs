using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Orders.DomainServices.Interfaces
{
    public interface ISendSMSService
    {
        Task TaskSendSMSServiceAsync(IConfiguration configuration, string to);
    }
}
