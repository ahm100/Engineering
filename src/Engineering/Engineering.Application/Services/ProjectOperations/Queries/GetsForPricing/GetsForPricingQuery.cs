using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsForPricing;

public record GetsForPricingQuery(
    long EmployerId,
    long ProjectId,
    long CostCenterId,
    string? ContractCode,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperation>>>;
