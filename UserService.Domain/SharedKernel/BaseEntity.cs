using System;

namespace UserService.Domain.SharedKernel
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}