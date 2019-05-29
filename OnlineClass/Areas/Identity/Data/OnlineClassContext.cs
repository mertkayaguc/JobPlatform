using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JobPlatform.Models
{
    public class OnlineClassContext : IdentityDbContext<IdentityUser2, IdentityRole2, string>
    {
        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Like> Likes { get; set; }

        public virtual DbSet<Follow> Follows { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Job> Jobs { get; set; }

        public virtual DbSet<JobApplication> JobApplications { get; set; }

        public OnlineClassContext(DbContextOptions options) : base(options)
        {
        }

        // The call of the base constructor via ": base()" is still required
        public OnlineClassContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Startup.Configuration.GetConnectionString("JobPlatformContextConnection"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
