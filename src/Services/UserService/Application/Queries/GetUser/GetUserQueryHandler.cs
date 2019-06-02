using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserServiceDbContext _context;

        public GetUserQueryHandler(IUserServiceDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(x => x.Languages)
                .FirstOrDefaultAsync(x => x.Id == request.UserId,
                    cancellationToken);

            if (user == null) return null;

            var listOfLanguages = user.Languages.Select(language => new LanguagePreviewDto
                {
                    Id = language.Id,
                    LanguageCode = language.LanguageCode,
                    LanguageName = language.LanguageName,
                    LanguageNativeName = language.LanguageNativeName
                })
                .ToList();

            return new UserDto
            {
                Username = user.Username,
                Languages = listOfLanguages
            };
        }
    }
}