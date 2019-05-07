using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Infrastructure.Persistence;

namespace UserService.WebApi.Controllers
{
    public class AddNewLanguageCommand : IRequest
    {
        public Guid UserId { get; set; }
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public string LanguageNativeName { get; set; }
    }

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
            var user = await _context.Users.FindAsync(request.UserId);

            if (user == null)
            {
                return Unit.Value;
            }


            user.Languages.Add(new Domain.Users.Language {
                Id = request.Id,
                LanguageCode = request.LanguageCode,
                LanguageName = request.LanguageName,
                LanguageNativeName = request.LanguageNativeName
            });

            return Unit.Value;
        }
    }
}