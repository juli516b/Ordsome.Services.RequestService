using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Application.Commands.Login
{
    public class LoginCommand : IRequest<LoginToken>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
