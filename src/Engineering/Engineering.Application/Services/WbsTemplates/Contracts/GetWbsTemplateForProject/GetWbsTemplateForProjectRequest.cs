namespace Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateForProject;

public record GetWbsTemplateForProjectRequest(
    long ProjectId,
    long? ParentId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;