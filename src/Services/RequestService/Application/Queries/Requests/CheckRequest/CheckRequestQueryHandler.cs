using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.Mappings;
using Application.Interfaces;
using Application.Models;
using Domain.Requests;
using F23.StringSimilarity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Requests.CheckRequest
{
    public class CheckRequestQueryHandler : IRequestHandler<CheckRequestQuery, IEnumerable<AnswerDto>>
    {
        private readonly IRequestServiceDbContext _context;
        private readonly IRequestMappings _mapper;

        public CheckRequestQueryHandler(IRequestServiceDbContext context, IRequestMappings mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AnswerDto>> Handle(CheckRequestQuery request,
            CancellationToken cancellationToken)
        {
            var allTextToTranslate = await _context.Requests.Include(a => a.Answers).ToListAsync(cancellationToken);
            var requestsChecked = new List<Request>();
            foreach (var item in allTextToTranslate)
            {
                var similarity = CalculateSimilarity(item.TextToTranslate, request.TextToTranslate);
                if (similarity > 0.30) requestsChecked.Add(item);
            }

            var answersToReturn = new List<AnswerDto>();
            foreach (var requestToCheck in requestsChecked)
            foreach (var answer in requestToCheck.Answers)
            {
                await _context.Requests.FirstOrDefaultAsync(x => x.Id == answer.RequestId, cancellationToken);
                answersToReturn.Add(await _mapper.ToAnswerDTOAsync(answer));
            }

            return answersToReturn;
        }

        //TODO - look at this and how it returns similar request
        private static double CalculateSimilarity(string source, string target)
        {
            var d = new Damerau();
            if (source == null || target == null) return 0.0;

            if (source.Length == 0 || target.Length == 0) return 0.0;

            if (source == target) return 1.0;

            var stepsToSame = d.Distance(source, target);

            return 1.0 - stepsToSame / Math.Max(source.Length, target.Length);
        }
    }
}