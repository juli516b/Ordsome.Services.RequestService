using System.Collections.Generic;
using Domain.SharedKernel;

namespace Domain.Users
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<Language> Languages { get; set; }
    }
}