using Domain.SharedKernel;

namespace Domain.SeedWork
{
    //TODO - move to crosscuttingconcerns
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}