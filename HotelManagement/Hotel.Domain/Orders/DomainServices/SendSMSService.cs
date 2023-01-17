using Hotel.Domain.Orders.DomainServices.Interfaces;
using Hotel.SharedKernel.SMS;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Orders.DomainServices
{
    public class SendSMSService : ISendSMSService
    {
        private ISMS _smsUtil;
        public SendSMSService(ISMS sms)
        {
            _smsUtil = sms;
        }
        public async Task TaskSendSMSServiceAsync(IConfiguration _configuration, string to)
        {
            await _smsUtil.ConfigSMSAsync(_configuration);
            _smsUtil.SendAsync(to, _configuration["Twillio:Message"]);
        }
    }
}
