using System;
using System.Collections.Generic;
using System.Text;

namespace Ordsome.Services.CrossCuttingConcerns.Languages
{
    public static class LanguageValidationHelpers
    {
        public static bool BeALanguage(string arg)
        {
            ListOfLanguages listOfLanguages = new ListOfLanguages();

            var language = listOfLanguages.GetLanguageByCode(arg);

            if (language == null)
            {
                return false;
            }
            return true;
        }
    }
}
