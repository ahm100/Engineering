namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPORequirements;

public record GetPORequirementsRequest(
    long ProjectOperationId) : IHttpRequest;