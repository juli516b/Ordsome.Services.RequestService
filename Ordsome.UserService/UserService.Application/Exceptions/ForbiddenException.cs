using System;

namespace UserService.Application.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string name, object key) : base($"Entity \"{name}\" ({key}) was forbidden the entity.") { }

    }
}