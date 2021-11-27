using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class EmailMessage
    {
        public MailAddressCollection From { get; set; }

        public MailAddressCollection To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public EmailMessage()
        {

        }

        public EmailMessage(MailAddressCollection From) {
            this.From = From;
            }
    }
}
