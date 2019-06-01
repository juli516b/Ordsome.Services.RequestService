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
    public class GetAnswerByRequestIdAndAnswerIdQuery : IRequest<AnswerDto>
    {
        public int AnswerId { get; set; }
        public int RequestId { get; set; }
    }
}
