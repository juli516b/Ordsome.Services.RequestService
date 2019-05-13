using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ordsome.Services.CrossCuttingConcerns.Languages;
using RequestService.Application.Interfaces;
using RequestService.Domain.Requests;
using RequestService.Infrastructure.Persistence;

namespace RequestService.Application.Commands.Requests.RequestCreation
{
    public class CreateRequestCommand : IRequest
    {
        public int LanguageOriginId { get; set; }
        public int LanguageTargetId { get; set; }
        public string TextToTranslate { get; set; }
        public Guid UserId { get; set; }
    }

    public class Handler : IRequestHandler<CreateRequestCommand, Unit>
    {
        private readonly RequestServiceDbContext _context;
        private readonly IMediator _mediator;

        public Handler(RequestServiceDbContext context, INotificationService notificationService, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            ListOfLanguages listOfLanguages = new ListOfLanguages();

            var getAndCheckIfLanguageOriginExists = listOfLanguages.GetLanguage(request.LanguageOriginId);

            var getAndCheckIfLanguageTargetExists = listOfLanguages.GetLanguage(request.LanguageTargetId);

            if (getAndCheckIfLanguageTargetExists == null)
            {
                throw new Exception();
            }

            if (getAndCheckIfLanguageOriginExists == null)
            {
                string emptyLanguage = "Not set";
                var entity = new Request
                {
                    LanguageTarget = getAndCheckIfLanguageTargetExists.LanguageName,
                    LanguageOrigin = emptyLanguage,
                    TextToTranslate = request.TextToTranslate,
                    UserId = request.UserId
                };

                _context.Requests.Add(entity);

            }
            else
            {
                var entity = new Request
                {
                    LanguageTarget = getAndCheckIfLanguageTargetExists.LanguageName,
                    LanguageOrigin = getAndCheckIfLanguageOriginExists.LanguageName,
                    TextToTranslate = request.TextToTranslate,
                    UserId = request.UserId
                };
            }

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}