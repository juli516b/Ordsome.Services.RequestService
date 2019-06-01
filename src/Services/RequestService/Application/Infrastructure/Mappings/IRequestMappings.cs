using Application.Models;
using Domain.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Mappings
{
    public interface IRequestMappings
    {
        RequestPreviewDto ToRequestPreviewDTO(Request request);
        Task<AnswerDto> ToAnswerDTOAsync(Answer answer);
    }
}
