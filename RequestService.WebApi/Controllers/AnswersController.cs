using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestService.Application.Commands.Answers.AnswerCreation;
using RequestService.Application.Commands.Answers.SetAnswerAsCorrectAnswer;
using System.Threading.Tasks;

namespace RequestService.WebApi.Controllers
{
    [Route("api/answer")]
    public class AnswersController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreateAnswerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPatch("isPreferred")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> IsPreferred([FromBody]SetAnswerAsCorrectAnswerCommand command)
        {
            await Mediator.Publish(command);

            return NoContent();
        }
    }

}
