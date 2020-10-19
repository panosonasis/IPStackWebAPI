using System;
using System.Collections.Generic;
using System.Text;

namespace IPStackExternalService.Models
{
    public class IPDetailsError
    {
        public bool success { get; set; }
        public IPError error { get; set; }
    }
}
