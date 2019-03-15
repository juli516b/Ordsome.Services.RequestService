using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestService.Application.Commands.Requests.RequestCreation;
using RequestService.Application.Queries.Requests.GetRequests;
using RequestService.Application.Queries.Requests.GetRequestsWithoutAnswers;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RequestService.WebApi.Controllers
{
    public class RequestsController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RequestPreviewDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetRequestsQuery()).ConfigureAwait(false));
        }

        [HttpGet]
        public async Task<ActionResult<RequestsWithoutAnswersViewModel>> GetAllWithoutAnswers()
        {
            return Ok(await Mediator.Send(new GetRequestsWithoutAnswersListQuery()).ConfigureAwait(false));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreateRequestCommand command)
        {
            await Mediator.Send(command).ConfigureAwait(false);

            return NoContent();
        }
    }
}