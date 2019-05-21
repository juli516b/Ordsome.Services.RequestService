using Domain.Requests;
using Domain.SeedWork;

namespace Domain.Interfaces
{
    public interface IAnswerVoteRepository : IRepository<AnswerVote>
    {
        AnswerVote CreateAnswerVote(AnswerVote answerVote);
    }
}