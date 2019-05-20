using System;
using UserService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Users;

namespace UserService.Infrastructure.Persistence
{
    public class UserServiceDbContext : DbContext, IUserServiceDbContext
    {
        public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Language>()
            //    .Property(b => b.Id)
            //    .ValueGeneratedNever();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserServiceDbContext).Assembly);
        }
    }
}