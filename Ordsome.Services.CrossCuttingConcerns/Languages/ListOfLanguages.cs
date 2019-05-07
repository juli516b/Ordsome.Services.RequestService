using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Ordsome.Services.CrossCuttingConcerns.Languages
{
    public class ListOfLanguages
    {

        public List<LanguageDto> _list = JsonConvert.DeserializeObject<List<LanguageDto>> (File.ReadAllText ("..\\Ordsome.Services.CrossCuttingConcerns\\Languages\\languages.json"));

        public LanguageDto GetLanguage(int id) 
        {
            var language = _list.FirstOrDefault(x => x.Id == id);
            
            if (language == null)
                {
                    return null;
                }
            
            return language;
        }

    }
}