using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.Mappings;
using Application.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordsome.Services.CrossCuttingConcerns.Exceptions;

namespace Application.Queries.Answers.GetAnswersByUserId
{
    public class GetAnswersByUserIdQueryHandler : IRequestHandler<GetAnswersByUserIdQuery, ICollection<AnswerDto>>
    {
        private readonly IRequestServiceDbContext _context;
        private readonly IRequestMappings _mapper;

        public GetAnswersByUserIdQueryHandler(IRequestServiceDbContext context, IRequestMappings mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<AnswerDto>> Handle(GetAnswersByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var answers = await _context.Answers.Where(x => x.UserId == request.UserId).ToListAsync(cancellationToken);

            if (answers == null) throw new NotFoundException($"{request.UserId}", request);

            var answersToReturn = new List<AnswerDto>();

            foreach (var item in answers) answersToReturn.Add(await _mapper.ToAnswerDTOAsync(item));
            return answersToReturn;
        }
    }
}