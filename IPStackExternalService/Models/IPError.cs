using System;
using System.Collections.Generic;
using System.Text;

namespace IPStackExternalService.Models
{
    public class IPError
    {
        public int? code { get; set; }
        public string type { get; set; }
        public string info { get; set; }
    }
}
