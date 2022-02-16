using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Domain.Entities
{
    public class Mail
    {
        public string  To { get; set; }
        public string Subject { get; set; } = "Verify your student email";
        public string Text { get; set; }
    }
}
