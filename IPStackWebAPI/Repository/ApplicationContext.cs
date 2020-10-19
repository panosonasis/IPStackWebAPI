using IPStackExternalService.Models;
using IPStackWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPStackWebAPI.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<IPDetailsExtDTO> IPDetails { get; set; }
        public DbSet<Job> Job { get; set; }

    }
}
