using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Commands.Answers.SetAnswerAsCorrectAnswer;
using Application.Queries.Answers.GetAnswersByUserId;
using Application.Queries.Requests.GetAnswersByRequestId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/answers")]
    public class AnswersController : BaseController
    {
        /// <summary>
        ///     Sets a answer's bool 'isPreferred'.
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
        [ProducesResponseType(typeof(IEnumerable<AnswerDto>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult> GetAllAnswersByUserId(Guid userId)
        {
            return Ok(await Mediator.Send(new GetAnswersByUserIdQuery {UserId = userId}));
        }
    }
}