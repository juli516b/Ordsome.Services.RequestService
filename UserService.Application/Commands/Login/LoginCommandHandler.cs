using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserService.Infrastructure.Persistence;

namespace UserService.Application.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginToken>
    {
        private UserServiceDbContext _context;
        private IMediator _mediator;
        private IConfiguration _config;

        public LoginCommandHandler (UserServiceDbContext context, IMediator mediator, IConfiguration config)
        {
            _context = context;
            _mediator = mediator;
            _config = config;
        }
        public async Task<LoginToken> Handle (LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync (x => x.Username == request.Username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash (request.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            var claims = new []
            {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
                new Claim (ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey (Encoding.UTF8
                .GetBytes (_config.GetSection ("AppSettings:Secret").Value));

            var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler ();

            var token = tokenHandler.CreateToken (tokenDescriptor);

            LoginToken tokenToReturn = new LoginToken
            {
                Token = tokenHandler.WriteToken (token)
            };
            return tokenToReturn;
        }
        private bool VerifyPasswordHash (string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 (passwordSalt))
            {
                var computedHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
    }
}