using System.Collections.Generic;
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
        public int LanguageOrignId { get; set; }
        public string LanguageOriginCode { get; set; }
        public string LanguageOriginName { get; set; }
        public string LanguageOriginNativeName { get; set; }

        public string LanguageTargetCode { get; set; }
        public int LanguageTargetlId { get; set; }
        public string LanguageTargetName { get; set; }
        public string LanguageTargetNativeName { get; set; }

        public string TextToTranslate { get; set; }
    }

    public class Handler : IRequestHandler<CreateRequestCommand, Unit>
    {
        private readonly RequestServiceDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly IMediator _mediator;

        public Handler (RequestServiceDbContext context, INotificationService notificationService, IMediator mediator)
        {
            _context = context;
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async Task<Unit> Handle (CreateRequestCommand request, CancellationToken cancellationToken)
        {
            ListOfLanguages listOfLanguages = new ListOfLanguages ();

            var checkIfLanguageOriginExists = listOfLanguages.CheckIfLanguageExists (new LanguageDto
            {
                Id = request.LanguageOrignId,
                    LanguageCode = request.LanguageOriginCode,
                    LanguageName = request.LanguageOriginName,
                    LanguageNativeName = request.LanguageOriginNativeName
            });

            var checkIfLanguageTargetExists = listOfLanguages.CheckIfLanguageExists (new LanguageDto
            {
                Id = request.LanguageTargetlId,
                    LanguageCode = request.LanguageTargetCode,
                    LanguageName = request.LanguageTargetName,
                    LanguageNativeName = request.LanguageTargetNativeName
            });

            if (checkIfLanguageOriginExists == false || checkIfLanguageTargetExists == false)
            {
                return Unit.Value;
            }

            var entity = new Request
            {
                LanguageTarget = request.LanguageTargetName,
                LanguageOrigin = request.LanguageOriginName,
                TextToTranslate = request.TextToTranslate
            };

            _context.Requests.Add (entity);

            await _context.SaveChangesAsync (cancellationToken).ConfigureAwait (false);

            await _mediator.Publish (new RequestCreated { RequestId = entity.Id }).ConfigureAwait (false);

            return Unit.Value;
        }
    }
}