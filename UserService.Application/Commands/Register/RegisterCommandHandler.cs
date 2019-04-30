using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserService.Domain.Users;
using UserService.Infrastructure.Persistence;

namespace UserService.Application.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private UserServiceDbContext _context;
        private IMediator _mediator;

        public RegisterCommandHandler(UserServiceDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            Guid guidOutput;

            if (Guid.TryParse(request.Id, out guidOutput) == true)
            {
                if (_context.Users.FirstOrDefaultAsync(r => r.Id == guidOutput) == null)
                {
                    Guid newGuid = new Guid();
                    var userWithoutId = await MakeUser(newGuid, request.Username, request.Password);

                    await _context.Users.AddAsync(userWithoutId);
                }
                else
                {
                    var user = await MakeUser(guidOutput, request.Username, request.Password);

                    await _context.Users.AddAsync(user);
                }
            }

            await _context.SaveChangesAsync();

            return Unit.Value;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }

        private async Task<User> MakeUser(Guid id, string username, string password)
        {
            username = username.ToLower();

            if (await UserExists(username) == true)
            {
                throw new Exception();
            }

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
