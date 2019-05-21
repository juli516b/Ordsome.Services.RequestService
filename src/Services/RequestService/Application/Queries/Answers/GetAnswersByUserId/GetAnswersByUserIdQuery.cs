using System;
using System.Collections.Generic;
using Application.Models;
using MediatR;

namespace Application.Queries.Answers.GetAnswersByUserId
{
    public class GetAnswersByUserIdQuery : IRequest<ICollection<AnswerDto>>
    {
        public Guid UserId { get; set; }
    }
}