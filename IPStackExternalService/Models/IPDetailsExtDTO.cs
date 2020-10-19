using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IPStackExternalService.Models
{
    public class IPDetailsExtDTO : IPDetailsDTO
    {
        [Key]
        public string IP { get; set; }

    }
}
