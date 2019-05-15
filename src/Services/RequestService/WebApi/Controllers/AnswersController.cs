using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestService.Application.Commands.Answers.AnswerCreation;
using RequestService.Application.Commands.Answers.SetAnswerAsCorrectAnswer;
using RequestService.Application.Queries.Answers.GetanswersByUserId;
using RequestService.Application.Queries.Requests.GetAnswersByRequestId;

namespace RequestService.WebApi.Controllers
{
    [Route("api/answers")]
    public class AnswersController : BaseController
    {
        /// <summary>
        /// Creates an answer for a request.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateAnswerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Sets a answer's bool 'isPreferred'.
        /// </summary>
        [HttpPatch("isPreferred")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> IsPreferred([FromBody] SetAnswerAsCorrectAnswerCommand command)
        {
            await Mediator.Publish(command);

            return NoContent();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("/u/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<AnswerDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAllAnswersByUserId(Guid userId)
        {
            return Ok(await Mediator.Send(new GetAnswersByUserIdQuery { UserId = userId }));
        }
    }

}