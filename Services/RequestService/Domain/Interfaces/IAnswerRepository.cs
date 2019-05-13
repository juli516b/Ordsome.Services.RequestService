using Domain.SeedWork;
using RequestService.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RequestService.Domain.Interfaces
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        Task<IEnumerable<Answer>> GetAllAnswersBasedOnRequestId(int id);

        Task<int> GetCountOfAnswersByRequestId(int id);
        Answer CreateAnswer(Answer answer);
        Answer UpdateAnswer(Answer answer);

    }

}