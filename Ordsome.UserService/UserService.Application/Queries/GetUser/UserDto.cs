using System.Collections.Generic;

namespace UserService.Application.Queries.GetUser
{
    public class UserDto
    {
        public string Username { get; set; }
        public List<LanguagePreviewDto> Languages { get; set; }
    }
}