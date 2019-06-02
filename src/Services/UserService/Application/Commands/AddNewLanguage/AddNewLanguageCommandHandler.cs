using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordsome.Services.CrossCuttingConcerns.Exceptions;
using Ordsome.Services.CrossCuttingConcerns.Languages;

namespace Application.Commands.AddNewLanguage
{
    public class AddNewLanguageCommandHandler : IRequestHandler<AddNewLanguageCommand, Unit>
    {
        private readonly IUserServiceDbContext _context;

        public AddNewLanguageCommandHandler(IUserServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewLanguageCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(x => x.Languages)
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user == null) throw new NotFoundException(request.UserId.ToString(), request);

            var language = ListOfLanguages.GetLanguageByCode(request.LanguageCode);

            var languageToAdd = new Language

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