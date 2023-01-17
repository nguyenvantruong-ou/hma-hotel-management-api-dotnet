using Hotel.SharedKernel.SMS;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Hotel.Infrastructure.Utils
{
    public class SMS : ISMS
    {
        public string _accountSID { get; set; }
        public string _authToken { get; set; }
        public string _myPhone { get; set; }
        public async Task ConfigSMSAsync(IConfiguration configuration)
        {
            _accountSID = configuration["Twillio:AccountSID"];
            _authToken = configuration["Twillio:AuthToken"];
            _myPhone = configuration["Twillio:MyPhone"];
        }

        public async Task SendAsync(string to, string message)
        {
            TwilioClient.Init(_accountSID, _authToken);
            MessageResource.Create(
               body: message,
               from: new Twilio.Types.PhoneNumber(_myPhone),
               to: new Twilio.Types.PhoneNumber(to)
            );
            throw new NotImplementedException();
        }
    }
}
