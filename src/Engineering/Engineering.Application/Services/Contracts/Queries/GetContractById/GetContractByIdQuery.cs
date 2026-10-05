using Engineering.Domain.Entities.Contracts;

namespace Engineering.Application.Services.Contracts.Queries.GetContractById;

public record GetContractByIdQuery(
    long Id) : IQuery<Contract?>;