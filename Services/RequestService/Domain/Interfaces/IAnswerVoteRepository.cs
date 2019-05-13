using Domain.SeedWork;
using RequestService.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RequestService.Domain.Interfaces
{
    public interface IAnswerVoteRepository : IRepository<AnswerVote>
    {
        AnswerVote CreateAnswerVote(AnswerVote answerVote);
    }

}