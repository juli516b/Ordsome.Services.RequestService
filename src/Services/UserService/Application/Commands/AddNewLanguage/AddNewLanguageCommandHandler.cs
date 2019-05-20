using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordsome.Services.CrossCuttingConcerns.Languages;
using UserService.Application.Interfaces;
using UserService.Domain.Users;

namespace UserService.Application.Commands.AddNewLanguage
{
    public class AddNewLanguageCommandHandler : IRequestHandler<AddNewLanguageCommand, Unit>
    {
        private readonly IUserServiceDbContext _context;
        private readonly IMediator _mediator;

        public AddNewLanguageCommandHandler(IUserServiceDbContext context, IMediator mediator)
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

            var language = listOfLanguages.GetLanguageById(request.LanguageId);

            Language languageToAdd = new Language

            {
                LanguageCode = language.LanguageCode,
                LanguageName = language.LanguageName,
                LanguageNativeName = language.LanguageNativeName
            };

            user.Languages.Add(languageToAdd);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}