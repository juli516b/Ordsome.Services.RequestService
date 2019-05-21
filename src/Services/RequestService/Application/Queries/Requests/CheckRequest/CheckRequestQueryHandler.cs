using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.Mappings;
using Application.Interfaces;
using Application.Models;
using F23.StringSimilarity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Requests.CheckRequest
{
    public class CheckRequestQueryHandler : IRequestHandler<CheckRequestQuery, IEnumerable<RequestPreviewDto>>
    {
        private readonly IRequestServiceDbContext _context;
        private readonly IMediator _mediator;

        public CheckRequestQueryHandler(IRequestServiceDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IEnumerable<RequestPreviewDto>> Handle(CheckRequestQuery request,
            CancellationToken cancellationToken)
        {
            var allTextToTranslate = await _context.Requests.ToListAsync(cancellationToken);
            var requestToReturn = new List<RequestPreviewDto>();
            foreach (var item in allTextToTranslate)
            {
                var similarity = CalculateSimilarity(item.TextToTranslate, request.TextToTranslate);
                if (similarity > 0.30) requestToReturn.Add(RequestMappings.ToRequestPreviewDTO(item));
            }

            return requestToReturn;
        }

        //TODO - look at this and how it returns similar request
        private double CalculateSimilarity(string source, string target)
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