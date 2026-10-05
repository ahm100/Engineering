using Engineering.Application.Services.WbsTemplates.Contracts.GetFltrWbsTemplate;

namespace Engineering.Application.Services.WbsTemplates.Queries.GetFltrWbsTemplate;

public record GetFltrWbsTemplateQuery(
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<GetFltrWbsTemplateResponse?>;