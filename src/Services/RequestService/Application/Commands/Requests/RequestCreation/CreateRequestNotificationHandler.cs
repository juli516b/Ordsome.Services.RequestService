using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Requests;
using MediatR;
using Ordsome.Services.CrossCuttingConcerns.Exceptions;
using Ordsome.Services.CrossCuttingConcerns.Languages;

namespace Application.Commands.Requests.RequestCreation
{
    public class CreateRequestNotificationHandler : INotificationHandler<CreateRequestCommand>
    {
        private readonly IRequestServiceDbContext _context;
        private readonly IMediator _mediator;

        public CreateRequestNotificationHandler(IRequestServiceDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            var languageOrigin = ListOfLanguages.GetLanguageByCode(request.LanguageOriginCode);

            var languageTarget = ListOfLanguages.GetLanguageByCode(request.LanguageTargetCode);

            if (languageTarget == null) throw new NotFoundException($"{request.LanguageTargetCode}", languageOrigin);

            switch (languageOrigin)
            {
                case null:
                {
                    const string emptyLanguage = "Not set";
                    var entity = new Request
                    {
                        LanguageTarget = languageTarget.LanguageCode,
                        LanguageOrigin = emptyLanguage,
                        TextToTranslate = Regex.Replace(request.TextToTranslate, @"\s+", " ").Trim(),
                        UserId = request.UserId
                    };
                    _context.Requests.Add(entity);
                    break;
                }

                default:
                {
                    var entity = new Request
                    {
                        LanguageTarget = languageTarget.LanguageCode,
                        LanguageOrigin = languageOrigin.LanguageCode,
                        TextToTranslate = Regex.Replace(request.TextToTranslate, @"\s+", " ").Trim(),
                        UserId = request.UserId
                    };

                    _context.Requests.Add(entity);
                    break;
                }
            }

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}