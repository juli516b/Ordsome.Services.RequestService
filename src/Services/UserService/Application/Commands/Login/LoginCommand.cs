using MediatR;

namespace Application.Commands.Login
{
    public class LoginCommand : IRequest<LoginToken>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}