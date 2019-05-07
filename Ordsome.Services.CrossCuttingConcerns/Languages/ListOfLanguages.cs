using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Ordsome.Services.CrossCuttingConcerns.Languages {
    public class ListOfLanguages {

        public List<LanguageDto> _list = JsonConvert.DeserializeObject<List<LanguageDto>>(File.ReadAllText("..\\Ordsome.Services.CrossCuttingConcerns\\Languages\\languages.json"));

        public bool CheckIfLanguageExists(LanguageDto languageToCheck) 
        {
            if (_list.Contains(languageToCheck))
                return true;
            return false;
        }

    }
}