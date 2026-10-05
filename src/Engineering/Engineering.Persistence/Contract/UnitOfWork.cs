using Gita.Backend.Shared.Domain.Base.Events;
using Gita.Backend.Shared.Persistence.Contracts;
using MediatR;

namespace Engineering.Persistence.Contract;

public class UnitOfWork : BaseUnitOfWork<EngineeringDBContext>
{
    public UnitOfWork(EngineeringDBContext context, IPublisher publisher) : base(context, publisher)
    {
    }

    protected override Task Publish(IDomainEvent domainEvent)
    {
        return Task.CompletedTask;
    }
}