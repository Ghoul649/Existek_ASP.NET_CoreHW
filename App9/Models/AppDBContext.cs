using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App9.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Database=App9;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(i => i.Articles)
                .WithOne(i => i.Author);

            modelBuilder.Entity<UserInfo>()
                .HasKey(i => i.UserId);

            modelBuilder.Entity<User>()
                .HasOne(i => i.UserInfo)
                .WithOne(i => i.User);

            modelBuilder.Entity<Article>()
                .HasMany(i => i.RelatedArticlesTo)
                .WithMany(i => i.RelatedArticlesFrom);
                
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
    }
}
