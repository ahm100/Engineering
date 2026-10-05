using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailWithProductSupply;

public record GetsProjectOperationDetailWithProductSupplyQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;