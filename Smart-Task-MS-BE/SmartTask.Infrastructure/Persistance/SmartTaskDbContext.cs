using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartTask.Domain.Entities;
using SmartTask.Infrastructure.Identity;

namespace SmartTask.Infrastructure.Persistance
{
    public class SmartTaskDbContext : IdentityDbContext<ApplicationUser>
    {
        public SmartTaskDbContext(DbContextOptions<SmartTaskDbContext> options) : base(options)
        {
            
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
    }
}