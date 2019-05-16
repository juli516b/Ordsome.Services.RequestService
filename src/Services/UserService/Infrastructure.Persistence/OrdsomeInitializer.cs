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
            Guid user1guid = Guid.NewGuid();
            string user1name = "bob";
            string user1password = "123123123";
            Guid user2guid = Guid.NewGuid();
            string user2name = "bobbob";
            string user2password = "123123123";
            Guid user3guid = Guid.NewGuid();
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
