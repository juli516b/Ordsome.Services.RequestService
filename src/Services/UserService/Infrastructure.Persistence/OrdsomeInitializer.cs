using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Domain.Users;

namespace Infrastructure.Persistence
{
    public class OrdsomeInitializer
    {
        public static void Initialize(UserServiceDbContext context)
        {
            var initializer = new OrdsomeInitializer();
            initializer.SeedEverything(context);
        }

        private void SeedEverything(UserServiceDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any()) return;
            SeedUsers(context);
        }

        private void SeedUsers(UserServiceDbContext context)
        {
            var user1guid = Guid.Parse("8e3f52d0-ee7e-4353-8941-ab1b75bbdf76");
            var user1name = "user1";
            var user1password = "123";
            var user2guid = Guid.Parse("50c41b86-1620-4912-9b5d-561b34dc27f7");
            var user2name = "user2";
            var user2password = "123";
            var user3guid = Guid.Parse("0e8e8abd-0f47-4164-9a1d-55ed125db66b");
            var user3name = "user3";
            var user3password = "123";
            context.Users.Add(MakeUser(user1guid, user1name, user1password));
            context.Users.Add(MakeUser(user2guid, user2name, user2password));
            context.Users.Add(MakeUser(user3guid, user3name, user3password));
            context.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private User MakeUser(Guid id, string username, string password)
        {
            username = username.ToLower();

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            var user = new User
            {
                Id = id,
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            return user;
        }
    }
}