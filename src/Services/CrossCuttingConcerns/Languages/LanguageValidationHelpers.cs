﻿namespace Ordsome.Services.CrossCuttingConcerns.Languages
{
    public static class LanguageValidationHelpers
    {
        public static bool BeALanguage(string arg)
        {
            var listOfLanguages = new ListOfLanguages();

            var language = listOfLanguages.GetLanguageByCode(arg);

            return language != null;
        }
    }
}