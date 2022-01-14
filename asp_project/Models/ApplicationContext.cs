using asp_project.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_project.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
    }
}
