using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Commands.Answers.VoteOnAnswer;
using Application.Commands.Requests.AnswerCreation;
using Application.Commands.Requests.CloseRequest;
using Application.Commands.Requests.RequestCreation;
using Application.Queries.Requests.GetAnswersByRequestId;
using Application.Queries.Requests.GetCountOfAnswersByRequestId;
using Application.Queries.Requests.GetRequest;
using Application.Queries.Requests.GetRequests;
using Application.Queries.Requests.GetRequestsByUserId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordsome.Services.CrossCuttingConcerns.Languages;

namespace WebApi.Controllers
{
    [Route("api/requests")]
    public class RequestsController : BaseController
    {
        /// <summary>
        ///     Gets all requests submitted by users
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<RequestPreviewDto>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult> GetAll([FromQuery] GetRequestsQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        ///     Gets all requests based on UserId
        /// </summary>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("u/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<RequestPreviewDto>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult> GetAllRequestsByUserId(Guid userId)
        {
            return Ok(await Mediator.Send(new GetRequestsByUserIdQuery {UserId = userId}));
        }

        /// <summary>
        ///     Get all languages listed in our API.
        /// </summary>
        [HttpGet("languages")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IList<LanguageDto>), (int) HttpStatusCode.OK)]
        public IActionResult GetAllLanguages()
        {
            var listOfLanguages = new ListOfLanguages();
            return Ok(listOfLanguages.GetList());
        }

        /// <summary>
        ///     Gets a request based on its id.
        /// </summary>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RequestPreviewDto), (int) HttpStatusCode.OK)]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetRequestQuery {RequestId = id}));
        }

        /// <summary>
        ///     Gets all answers for a request based on Id
        /// </summary>
        [HttpGet("{id}/answers")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<AnswerDto>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult> GetAnswersByRequestId(int id)
        {
            return Ok(await Mediator.Send(new GetAnswersByRequestIdQuery {RequestId = id}));
        }

        /// <summary>
        ///     Gets the count of answers.
        /// </summary>
        [HttpGet("{id}/answers/count")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CountOfAnswersDto), (int) HttpStatusCode.OK)]
        public async Task<ActionResult> GetCountOfAnswersByRequestId(int id)
        {
            return Ok(await Mediator.Send(new GetCountOfAnswersByRequestIdQuery {RequestId = id}));
        }

        /// <summary>
        ///     Create a request.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateRequestCommand command)
        {
            await Mediator.Send(command).ConfigureAwait(false);

            return NoContent();
        }

        /// <summary>
        ///     Create an answer for a request.
        /// </summary>
        [HttpPost("{id}/answers")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateAnswer([FromBody] CreateAnswerCommand command)
        {
            await Mediator.Send(command).ConfigureAwait(false);

            return NoContent();
        }

        /// <summary>
        ///     A user can vote on one of the answers already supplied. (Logic: but not one of his own)
        /// </summary>
        [HttpPost("{id}/answers/{answerId}/vote")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> VoteOnAnswer([FromBody] VoteOnAnswerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        ///     Sets the bool 'isClosed' for a request.
        /// </summary>
        [HttpPatch("isClosed")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CloseRequest([FromBody] CloseRequestCommand command)
        {
            await Mediator.Send(command).ConfigureAwait(false);

            return NoContent();
        }
    }
}