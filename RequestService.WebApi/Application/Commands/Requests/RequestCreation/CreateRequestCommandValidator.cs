using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestService.WebApi.Application.Commands.Requests.RequestCreation
{
    public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestCommandValidator()
        {
            RuleFor(x => x.LagnuageTarget).NotEmpty().NotNull();
            RuleFor(x => x.TextToTranslate).NotEmpty().NotNull();
        }
    }
}
