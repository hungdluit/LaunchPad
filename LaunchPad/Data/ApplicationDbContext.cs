using LaunchPad.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LaunchPad.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
                
        }

        public DbSet<Script> Scripts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserCategory> UserCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                .HasKey(x => x.Id);
            
            modelBuilder.Entity<Job>()
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Script>()
                .HasKey(x => x.Id);
            
            modelBuilder.Entity<Script>()
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
            
            modelBuilder.Entity<User>()
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Role>()
                .HasKey(x => x.Id);
            
            modelBuilder.Entity<Role>()
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<UserRole>()
                .HasKey(x => x.Id);
            
            modelBuilder.Entity<UserRole>()
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UserRole>()
                .HasIndex(r => new { r.UserId, r.RoleId })
                .IsUnique();

            modelBuilder.Entity<UserRole>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(r => r.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(r => r.RoleId);
            
            modelBuilder.Entity<UserCategory>()
                .HasKey(x => x.Id);
            
            modelBuilder.Entity<UserCategory>()
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UserCategory>()
                .HasIndex(r => new { r.UserId, r.CategoryId })
                .IsUnique();

            modelBuilder.Entity<UserCategory>()
                .HasOne(r => r.User)
                .WithMany(r => r.Categories)
                .HasForeignKey(r => r.UserId);
        }
    }
}
