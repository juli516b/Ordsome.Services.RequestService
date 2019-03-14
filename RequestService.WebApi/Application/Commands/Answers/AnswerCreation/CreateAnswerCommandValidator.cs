using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestService.WebApi.Application.Commands.Requests.AnswerCreation
{
    public class CreateAnswerCommandValidator : AbstractValidator<CreateAnswerCommand>
    {
        public CreateAnswerCommandValidator()
        {
            RuleFor(x => x.TextTranslated).NotEmpty().NotNull();
        }
    }
}
