using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace TestTask_4A_MVC.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public string User { get; set; }
        public DateTime RequestDateTime { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
