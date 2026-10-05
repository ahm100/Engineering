using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectContractors;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectContractors;

public record GetProjectContractorsQuery(
    long ProjectId,
    int PageIndex,
    int PageSize) : IQuery<GetProjectContractorsResponse?>;