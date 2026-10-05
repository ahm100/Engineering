using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPORequirements;

namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.GetPORequirements;

public record GetPORequirementsQuery(
    long ProjectOperationId) : IQuery<GetPORequirementsResponse?>;