using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Models;

namespace User.Api.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>()
                .ToTable("Users")
                .HasKey(x => x.ID);

            builder.Entity<UserPorperty>().Property(x => x.Key).HasMaxLength(100);
            builder.Entity<UserPorperty>().Property(x => x.Value).HasMaxLength(100);
            builder.Entity<UserPorperty>()
                .ToTable("UserPorperty")
                .HasKey(x => new { x.Key, x.AppUserID, x.Value });

            builder.Entity<UserTag>().Property(x => x.Tag).HasMaxLength(100);
            builder.Entity<UserTag>()
                .ToTable("UserTags")
                .HasKey(x => new { x.UserID, x.Tag });

            builder.Entity<BPFile>()
                .ToTable("UserBPFiles")
                .HasKey(x => x.ID);

            base.OnModelCreating(builder);
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserPorperty> UserPorpertys { get; set; }
    }
}
