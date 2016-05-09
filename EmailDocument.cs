using System.Collections.Generic;

namespace EmailServiceLibrary.Models
{
   public class EmailDocument
    {
        public List<string> To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string DisplayName { get; set; }
    }
}
