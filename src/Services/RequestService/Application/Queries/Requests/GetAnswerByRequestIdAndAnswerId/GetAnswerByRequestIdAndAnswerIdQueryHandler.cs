using Application.Infrastructure.Mappings;
using Application.Interfaces;
using Application.Models;
using Domain.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordsome.Services.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Requests.GetAnswerByRequestIdAndAnswerId
{
    public class GetAnswerByRequestIdAndAnswerIdQueryHandler : IRequestHandler<GetAnswerByRequestIdAndAnswerIdQuery, AnswerDto>
    {
        private readonly IRequestServiceDbContext _context;

        public GetAnswerByRequestIdAndAnswerIdQueryHandler(IRequestServiceDbContext context)
        {
            _context = context;
        }
        public async Task<AnswerDto> Handle(GetAnswerByRequestIdAndAnswerIdQuery request, CancellationToken cancellationToken)
        {
            var requestResult = await _context.Requests.Include(x => x.Answers).FirstOrDefaultAsync(x => x.Id == request.RequestId);

            if (requestResult == null)
            {
                throw new NotFoundException(request.RequestId.ToString(), request);
            }

            var answer = requestResult.Answers.FirstOrDefault(x => x.Id == request.AnswerId);

            return RequestMappings.ToAnswerDTO(answer, requestResult.TextToTranslate);
        }
    }
}
