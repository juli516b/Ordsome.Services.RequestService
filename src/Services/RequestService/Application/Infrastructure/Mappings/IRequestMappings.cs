using System.Threading.Tasks;
using Application.Models;
using Domain.Requests;

namespace Application.Infrastructure.Mappings
{
    public interface IRequestMappings
    {
        RequestPreviewDto ToRequestPreviewDTO(Request request);
        Task<AnswerDto> ToAnswerDTOAsync(Answer answer);
    }
}