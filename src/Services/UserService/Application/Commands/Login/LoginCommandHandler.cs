using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Ordsome.Services.CrossCuttingConcerns.Exceptions;

namespace Application.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginToken>
    {
        private readonly IConfiguration _config;
        private readonly IUserServiceDbContext _context;
        private readonly IMediator _mediator;

        public LoginCommandHandler(IUserServiceDbContext context, IMediator mediator, IConfiguration config)
        {
            _context = context;
            _mediator = mediator;
            _config = config;
        }

        public async Task<LoginToken> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username,
                cancellationToken);

            if (user == null) throw new NotFoundException($"{request.Username}", user);

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new ForbiddenException($"{request.Username}", user);

            var tokenToReturn = MakeTokenToReturn(user);

            return tokenToReturn;
        }

        private LoginToken MakeTokenToReturn(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Secret").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenToReturn = new LoginToken
            {
                Token = "Bearer " + tokenHandler.WriteToken(token)
            };
            return tokenToReturn;
        }

        private static bool VerifyPasswordHash(string password, IReadOnlyList<byte> passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return !computedHash.Where((t, i) => t != passwordHash[i]).Any();
            }
        }
    }
}