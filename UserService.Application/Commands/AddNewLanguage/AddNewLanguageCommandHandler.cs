using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordsome.Services.CrossCuttingConcerns.Languages;
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

            ListOfLanguages listOfLanguages = new ListOfLanguages();

            var language = listOfLanguages.GetLanguage(request.LanguageId);

            Language languageToAdd = new Language
            
            {
                LanguageCode = language.LanguageCode,
                LanguageName = language.LanguageName,
                LanguageNativeName = language.LanguageNativeName
            };

            user.Languages.Add(languageToAdd);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}