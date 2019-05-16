using System;
using System.Collections.Generic;
using System.Text;

namespace Ordsome.Services.CrossCuttingConcerns.Languages
{
    public class LanguageValidationHelpers
    {
        public bool BeALanguage(string arg)
        {
            ListOfLanguages listOfLanguages = new ListOfLanguages();

            var language = listOfLanguages.GetLanguageByCode(arg);

            return language == null;
        }
    }
}
