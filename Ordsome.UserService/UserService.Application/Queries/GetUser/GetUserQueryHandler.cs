using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Persistence;

namespace UserService.Application.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private UserServiceDbContext _context;
        private IMediator _mediator;

        public GetUserQueryHandler(UserServiceDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(x => x.Languages).FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user == null)
            {
                return null;
            }

            List<LanguagePreviewDto> listOfLanguages = new List<LanguagePreviewDto>();

            foreach (var language in user.Languages)
            {
                LanguagePreviewDto langaugeToAdd = new LanguagePreviewDto
                {
                    Id = language.Id,
                    LanguageCode = language.LanguageCode,
                    LanguageName = language.LanguageName,
                    LanguageNativeName = language.LanguageNativeName
                };

                listOfLanguages.Add(langaugeToAdd);
            }

            return new UserDto
            {
                Username = user.Username,
                    Languages = listOfLanguages
            };
        }
    }
}