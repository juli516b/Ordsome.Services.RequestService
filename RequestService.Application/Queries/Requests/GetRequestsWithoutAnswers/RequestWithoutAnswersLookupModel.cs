using RequestService.Application.Interfaces.Mapping;

namespace RequestService.Application.Queries.Requests.GetRequestsWithoutAnswers
{
    public class RequestWithoutAnswersLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string TextToTranslate { get; set; }
        public string LanguageOrigin { get; set; }
        public string LanguageTarget { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Request, RequestWithoutAnswersLookupModel>()
                .ForMember(rDTO => rDTO.Id, opt => opt.MapFrom(r => r.Id))
                .ForMember(rDTO => rDTO.LanguageOrigin, opt => opt.MapFrom(r => r.LanguageOrigin))
                .ForMember(rDTO => rDTO.LanguageTarget, opt => opt.MapFrom(r => r.LanguageTarget))
                .ForMember(rDTO => rDTO.TextToTranslate, opt => opt.MapFrom(r => r.TextToTranslate));
        }
    }
}
