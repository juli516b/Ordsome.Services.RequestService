using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Requests;
using MediatR;
using Ordsome.Services.CrossCuttingConcerns.Languages;

namespace Application.Commands.Requests.RequestCreation
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
        private readonly IRequestServiceDbContext _context;
        private readonly IMediator _mediator;

        public Handler(IRequestServiceDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            var listOfLanguages = new ListOfLanguages();

            var languageOrigin = ListOfLanguages.GetLanguageById(request.LanguageOriginId);

            var languageTarget = ListOfLanguages.GetLanguageById(request.LanguageTargetId);

            if (languageTarget == null) throw new NotFoundException($"{request.LanguageTargetId}", languageOrigin);

            if (languageOrigin == null)
            {
                const string emptyLanguage = "Not set";
                var entity = new Request
                {
                    LanguageTarget = languageTarget.LanguageCode,
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
                    LanguageTarget = languageTarget.LanguageCode,
                    LanguageOrigin = languageOrigin.LanguageCode,
                    TextToTranslate = request.TextToTranslate,
                    UserId = request.UserId
                };

                _context.Requests.Add(entity);
            }

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}