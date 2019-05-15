using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands.AddNewLanguage;
using UserService.Application.Commands.Login;
using UserService.Application.Commands.Register;
using UserService.Application.Queries.CheckUserId;
using UserService.Application.Queries.GetAnswersBasedOnUserId;
using UserService.Application.Queries.GetGuid;
using UserService.Application.Queries.GetRequestsBasedOnUserId;
using UserService.Application.Queries.GetUser;

namespace UserService.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : BaseController
    {
        /// <summary>
        /// Generates a Guid which is used for making requests.
        /// </summary>
        [HttpGet("new")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetGuidDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetNewGuid()
        {
            return Ok(await Mediator.Send(new GetGuidQuery()));
        }

        /// <summary>
        /// Registers a user to the database.
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> RegisterNewUser([FromBody] RegisterCommand command)
        {
            await Mediator.Send(command);

            return StatusCode(201);
        }

        /// <summary>
        /// Returns a JWToken which is used for authorization.
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginToken), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Login([FromBody] LoginCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Adds a new language to a user.
        /// </summary>
        [Authorize]
        [HttpPatch("newLanguage")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> NewLanguage([FromBody] AddNewLanguageCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetUserInformation(Guid id)
        {
            var result = await Mediator.Send(new GetUserQuery { UserId = id });

            return Ok(result);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("check/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CheckUserId(Guid id)
        {
            var result = await Mediator.Send(new CheckUserIdQuery { UserId = id });

            return Ok(result);
        }

        [HttpGet("{id}/requests")]
        [ProducesResponseType(typeof(ICollection<UserRequestsDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetRequestsBasedOnUserId(Guid id)
        {
            var result = await Mediator.Send(new GetRequestsBasedOnUserIdQuery { UserId = id });

            return Ok(result);
        }

        [HttpGet("{id}/answers")]
        [ProducesResponseType(typeof(ICollection<UserAnswersDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAnswersBasedOnUserId(Guid id)
        {
            var result = await Mediator.Send(new GetAnswersBasedOnUserIdQuery { UserId = id });

            return Ok(result);
        }
    }
}