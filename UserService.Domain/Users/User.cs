using System.Collections.Generic;
using UserService.Domain.SharedKernel;

namespace UserService.Domain.Users
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<Language> Languages  { get; set; }

    }
}
