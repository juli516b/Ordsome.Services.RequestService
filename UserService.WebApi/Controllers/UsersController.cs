using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using UserService.Application.Commands.AddNewLanguage;
using UserService.Application.Commands.Login;
using UserService.Application.Commands.Register;
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
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public IActionResult GetNewGuid()
        {
            return Ok(Guid.NewGuid());
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
            var result = await Mediator.Send(new GetUserQuery{ UserId = id });

            return Ok (result);
        }
    }
}