using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IPStackWebAPI.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid JobId { get; set; }
        public int? CompletedUpdates{ get; set; }
        public int? FailedUpdates { get; set; }
        public int? TotalUpdates { get; set; }
        public bool? HasFinished { get; set; }
    }
}
