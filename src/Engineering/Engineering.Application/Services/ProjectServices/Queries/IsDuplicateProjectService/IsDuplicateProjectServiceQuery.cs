namespace Engineering.Application.Services.ProjectServices.Queries.IsDuplicateProjectService;

public record IsDuplicateProjectServiceQuery(
    long? Id,
    long ProjectId,
    long ServiceInfoId,
    long ContractorId
    ) : IQuery<bool>;