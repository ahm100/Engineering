namespace Engineering.Application.Services.WbsTemplates.Contracts.GetFltrWbsTemplate;

public record GetFltrWbsTemplateRequest(
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;