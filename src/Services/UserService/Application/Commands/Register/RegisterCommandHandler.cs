using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordsome.Services.CrossCuttingConcerns.Exceptions;

namespace Application.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly IUserServiceDbContext _context;
        private readonly IMediator _mediator;

        public RegisterCommandHandler(IUserServiceDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.Id, out var outputGuid);

            await CheckIfGuidIsValid(request, cancellationToken, outputGuid);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task CheckIfGuidIsValid(RegisterCommand request, CancellationToken cancellationToken,
            Guid outputGuid)
        {
            if (outputGuid == Guid.Empty)
            {
                var newGuid = new Guid();
                var userWithoutId = await MakeUser(newGuid, request.Username, request.Password);

                await _context.Users.AddAsync(userWithoutId, cancellationToken);
            }
            else
            {
                var user = await MakeUser(outputGuid, request.Username, request.Password);
                await _context.Users.AddAsync(user, cancellationToken);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }

        private async Task<User> MakeUser(Guid id, string username, string password)
        {
            username = username.ToLower();

            if (await UserExists(username)) throw new ForbiddenException(username, username);


            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            return new User
            {
                Id = id,
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
        }
    }
}