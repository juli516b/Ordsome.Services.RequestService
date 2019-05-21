using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Requests;
using Domain.SeedWork;

namespace Domain.Interfaces
{
    public interface IRequestRepository : IRepository<Request>
    {
        Task<IEnumerable<Request>> GetAll();
        Task<IEnumerable<Request>> GetAllBasedOnUserId(Guid userId);
        Task<Request> GetById(int id);
        Task<int> NoOfVotes(Request request);
        Request CreateRequest(Request request);
        Request UpdateRequest(Request request);
    }
}