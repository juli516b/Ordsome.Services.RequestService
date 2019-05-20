using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Requests;
using Domain.SeedWork;

namespace Domain.Interfaces
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        Task<IEnumerable<Answer>> GetAllAnswersBasedOnRequestId(int id);

        Task<int> GetCountOfAnswersByRequestId(int id);
        Answer CreateAnswer(Answer answer);
        Answer UpdateAnswer(Answer answer);
    }
}