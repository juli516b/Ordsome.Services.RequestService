using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Domain.Users;

namespace UserService.Application.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private IUserServiceDbContext _context;
        private readonly IMediator _mediator;

        public RegisterCommandHandler(IUserServiceDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            bool parsedGuid = Guid.TryParse(request.Id, out Guid outputGuid);

            if (outputGuid == Guid.Empty)
            {
                Guid newGuid = new Guid();
                var userWithoutId = await MakeUser(newGuid, request.Username, request.Password);

                await _context.Users.AddAsync(userWithoutId);
            }
            else
            {
                var user = await MakeUser(outputGuid, request.Username, request.Password);
                await _context.Users.AddAsync(user);
            }
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
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


            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

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