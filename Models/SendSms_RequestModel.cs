using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendSMS.Asp.Net.Core.Models
{
    public class SendSms_RequestModel
    {
        public string Signatute { get; set; }
        public string Mobile { get; set; }
        public string Text { get; set; }
    }
}
