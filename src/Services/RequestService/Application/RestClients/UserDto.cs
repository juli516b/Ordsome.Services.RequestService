using System.Collections.Generic;
using Ordsome.Services.CrossCuttingConcerns.Languages;

namespace Application.RestClients
{
    public class UserDto
    {
        public string Username { get; set; }
        public IList<LanguageDto> Languages { get; set; }
    }
}