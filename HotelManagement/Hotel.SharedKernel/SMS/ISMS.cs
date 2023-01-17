using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.SharedKernel.SMS
{
    public interface ISMS
    {
        Task ConfigSMSAsync(IConfiguration Configuration);
        Task SendAsync(string to, string message);
    }
}
