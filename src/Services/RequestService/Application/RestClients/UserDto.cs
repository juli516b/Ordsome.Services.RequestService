using Ordsome.Services.CrossCuttingConcerns.Languages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.RestClients
{
    public class UserDto
    {
        public string Username { get; set; }
        public IList<LanguageDto> Langauges { get; set; }
    }
}
