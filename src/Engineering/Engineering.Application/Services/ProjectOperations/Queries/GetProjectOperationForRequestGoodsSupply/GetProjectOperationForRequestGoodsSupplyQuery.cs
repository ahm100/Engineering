using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationForRequestGoodsSupply;

public record GetProjectOperationForRequestGoodsSupplyQuery(
    long Id
    ) : IQuery<ProjectOperation>;