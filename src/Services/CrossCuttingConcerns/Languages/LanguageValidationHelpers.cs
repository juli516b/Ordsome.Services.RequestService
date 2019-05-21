namespace Ordsome.Services.CrossCuttingConcerns.Languages
{
    public static class LanguageValidationHelpers
    {
        public static bool BeALanguageByCode(string arg)
        {
            var language = ListOfLanguages.GetLanguageByCode(arg);

            return language != null;
        }

        public static bool BeALanguageById(int id)
        { 
            var language = ListOfLanguages.GetLanguageById(id);

            return language != null;
        }    
    }
}