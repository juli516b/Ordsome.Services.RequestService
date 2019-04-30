using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using UserService.Application.Commands.Login;
using UserService.Application.Commands.Register;

namespace UserService.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpGet("new")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public IActionResult GetNewGuid()
        {
            return Ok(Guid.NewGuid());
        }

        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> RegisterNewUser([FromBody] RegisterCommand command)
        {
            await Mediator.Send(command);

            return StatusCode(201);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Login([FromBody] LoginCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
