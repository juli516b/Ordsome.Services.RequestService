using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordsome.Services.CrossCuttingConcerns.Languages;
using RequestService.Application.Commands.Answers.AnswerCreation;
using RequestService.Application.Commands.Answers.SetAnswerAsCorrectAnswer;
using RequestService.Application.Commands.Requests.CloseRequest;
using RequestService.Application.Commands.Requests.RequestCreation;
using RequestService.Application.Queries.Requests.GetAnswersByRequestId;
using RequestService.Application.Queries.Requests.GetCountOfAnswersByRequestId;
using RequestService.Application.Queries.Requests.GetRequest;
using RequestService.Application.Queries.Requests.GetRequests;

namespace RequestService.WebApi.Controllers
{
    [Route ("api/requests")]
    public class RequestsController : BaseController
    {
        /// <summary>
        /// Gets all requests submitted by users
        /// </summary>
        [HttpGet("")]
        [Produces("application/json")]
        [ProducesResponseType (typeof (IEnumerable<RequestPreviewDto>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult> GetAll ()
        {
            return Ok (await Mediator.Send (new GetRequestsQuery ()).ConfigureAwait (false));
        }

        /// <summary>
        /// Get all languages listed in our API.
        /// </summary>
        [HttpGet ("languages")]
        [Produces("application/json")]
        [ProducesResponseType (typeof (ListOfLanguages), (int) HttpStatusCode.OK)]
        public IActionResult GetAllLanguages ()
        {
            ListOfLanguages listOfLanguages = new ListOfLanguages ();
            return Ok (listOfLanguages._list);
        }

        /// <summary>
        /// Gets a request based on its id.
        /// </summary>
        [HttpGet ("{id}")]
        [Produces("application/json")]
        [ProducesResponseType (typeof (RequestPreviewDto), (int) HttpStatusCode.OK)]
        public async Task<ActionResult> GetById (int id)
        {
            return Ok (await Mediator.Send (new GetRequestQuery { RequestId = id }));
        }

        /// <summary>
        /// Gets all answers for a request based on Id
        /// </summary>
        [HttpGet ("{id}/answers")]
        [Produces("application/json")]
        [ProducesResponseType (typeof (IEnumerable<AnswerDto>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult> GetAnswersByRequestId (int id)
        {
            return Ok (await Mediator.Send (new GetAnswersByRequestIdQuery { RequestId = id }));
        }

        /// <summary>
        /// Gets the count of answers.
        /// </summary>

        [HttpGet ("{id}/answers/count")]
        [Produces("application/json")]
        [ProducesResponseType (typeof (CountOfAnswersDto), (int) HttpStatusCode.OK)]
        public async Task<ActionResult> GetCountOfAnswersByRequestId (int id)
        {
            return Ok (await Mediator.Send (new GetCountOfAnswersByRequestIdQuery { RequestId = id }));
        }

        // [HttpPatch("{id}/answers/{answerId}/{isPreferred}")]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // [ProducesDefaultResponseType]
        // public async Task<IActionResult> SetAnswerAsCorrectAnswer([FromBody] int id, int answerId, bool isPreferred)
        // {
        //     await Mediator.Publish(new SetAnswerAsCorrectAnswerCommand { RequestId = id, AnswerId = answerId, IsPreferred = isPreferred });

        //     return NoContent();
        // }

        //[HttpGet()]
        //public async Task<ActionResult<RequestsWithoutAnswersViewModel>> GetAllWithoutAnswers()
        //{
        //    return Ok(await Mediator.Send(new GetRequestsWithoutAnswersListQuery()).ConfigureAwait(false));
        //}

        /// <summary>
        /// Create a request.
        /// </summary>
        [HttpPost]
        [ProducesResponseType (StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create ([FromBody] CreateRequestCommand command)
        {
            await Mediator.Send (command).ConfigureAwait (false);

            return NoContent();
        }

        /// <summary>
        /// Create an answer for a request.
        /// </summary>
        [HttpPost ("{id}/answer")]
        [ProducesResponseType (StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateAnswer ([FromBody] CreateAnswerCommand command)
        {
            await Mediator.Send (command).ConfigureAwait (false);

            return NoContent ();
        }

        /// <summary>
        /// Sets the bool 'isClosed' for a request.
        /// </summary>
        [HttpPatch ("isClosed")]
        [ProducesResponseType (StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CloseRequest ([FromBody] CloseRequestCommand command)
        {
            await Mediator.Send (command).ConfigureAwait (false);

            return NoContent ();
        }
    }
}