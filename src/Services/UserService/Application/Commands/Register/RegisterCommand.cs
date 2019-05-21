using MediatR;

namespace Application.Commands.Register
{
    public class RegisterCommand : IRequest
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}