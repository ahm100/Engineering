using Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateForProject;

namespace Engineering.Application.Services.WbsTemplates.Queries.GetWbsTemplateForProject;

public record GetWbsTemplateForProjectQuery(
    long ProjectId,
    long? ParentId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<GetWbsTemplateForProjectResponse?>;