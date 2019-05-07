using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
        public async Task<Unit> Handle(AddNewLanguageCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(command.UserId);

            if (user == null)
            {
                return Unit.Value;
            }

            
            user.Languages.Add($"{command.LanguageNativeName}, {command.LanguageName}");

            return Unit.Value;
        }
    }
}