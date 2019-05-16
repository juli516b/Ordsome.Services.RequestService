using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Domain.Users;
using UserService.Infrastructure.Persistence;

namespace Infrastructure.Persistence
{
    public class OrdsomeInitializer
    {
        private readonly Dictionary<int, User> Requests = new Dictionary<int, User>();

        public static void Initialize(UserServiceDbContext context)
        {
            var initializer = new OrdsomeInitializer();
            initializer.SeedEverything(context);
        }

        private void SeedEverything(UserServiceDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }
            SeedUsers(context);
        }

        public void SeedUsers(UserServiceDbContext context)
        {
            Guid user1guid = Guid.Parse("8e3f52d0-ee7e-4353-8941-ab1b75bbdf76");
            string user1name = "bob";
            string user1password = "123123123";
            Guid user2guid = Guid.Parse("50c41b86-1620-4912-9b5d-561b34dc27f7");
            string user2name = "bobbob";
            string user2password = "123123123";
            Guid user3guid = Guid.Parse("0e8e8abd-0f47-4164-9a1d-55ed125db66b");
            string user3name = "bobbobbob";
            string user3password = "123123123";
            context.Users.Add(MakeUser(user1guid, user1name, user1password));
            context.Users.Add(MakeUser(user2guid, user2name, user2password));
            context.Users.Add(MakeUser(user3guid, user3name, user3password));
            context.SaveChanges();
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        private User MakeUser(Guid id, string username, string password)
        {
            username = username.ToLower();

            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            User user = new User
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
