using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestService.Application.Commands.Answers.AnswerCreation;
using RequestService.Application.Commands.Requests.RequestCreation;
using RequestService.Application.Queries.Requests.GetAnswersByRequestId;
using RequestService.Application.Queries.Requests.GetCountOfAnswersByRequestId;
using RequestService.Application.Queries.Requests.GetRequest;
using RequestService.Application.Queries.Requests.GetRequests;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RequestService.WebApi.Controllers
{
    [Route("api/request")]
    public class RequestsController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RequestPreviewDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetRequestsQuery()).ConfigureAwait(false));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RequestPreviewDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetRequestQuery { Id = id }));
        }

        [HttpGet("{id}/answers")]
        [ProducesResponseType(typeof(IEnumerable<AnswerDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAnswersByRequestId(int id)
        {
            return Ok(await Mediator.Send(new GetAnswersByRequestIdQuery { Id = id }));
        }

        [HttpGet("{id}/answers/count")]
        [ProducesResponseType(typeof(CountOfAnswersDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCountOfAnswersByRequestId(int id)
        {
            return Ok(await Mediator.Send(new GetCountOfAnswersByRequestIdQuery { Id = id }));
        }

        //[HttpGet()]
        //public async Task<ActionResult<RequestsWithoutAnswersViewModel>> GetAllWithoutAnswers()
        //{
        //    return Ok(await Mediator.Send(new GetRequestsWithoutAnswersListQuery()).ConfigureAwait(false));
        //}

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreateRequestCommand command)
        {
            await Mediator.Send(command).ConfigureAwait(false);

            return NoContent();
        }

        [HttpPost("{id}/answer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateAnswer([FromBody]CreateAnswerCommand command)
        {
            await Mediator.Send(command).ConfigureAwait(false);

            return NoContent();
        }

    }
}