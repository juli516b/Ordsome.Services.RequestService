using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Users;
using UserService.Infrastructure.Persistence;

namespace UserService.Application.Commands.AddNewLanguage
{
    public class AddNewLanguageCommandHandler : IRequestHandler<AddNewLanguageCommand, Unit>
    {
        private readonly UserServiceDbContext _context;
        private readonly IMediator _mediator;

        public AddNewLanguageCommandHandler(UserServiceDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(AddNewLanguageCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(x => x.Languages).FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user == null)
            {
                return Unit.Value;
            }

            Language languageToAdd = new Language
            {
                Id = request.Id,
                LanguageCode = request.LanguageCode,
                LanguageName = request.LanguageName,
                LanguageNativeName = request.LanguageNativeName
            };

            user.Languages.Add(languageToAdd);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}