using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsProposedPrice;

public record GetsProposedPriceQuery(
    long OperationInfoId,
    string? FilterData,
    DateTime? StartDate,
    DateTime? EndDate,
    long? EmployerId,
    long? CostCenterId,
    long? ProjectId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperation>>>;
