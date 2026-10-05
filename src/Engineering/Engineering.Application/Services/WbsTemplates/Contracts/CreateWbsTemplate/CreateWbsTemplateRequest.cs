namespace Engineering.Application.Services.WbsTemplates.Contracts.CreateWbsTemplate;

public record CreateWbsTemplateRequest(
    string Title,
    string Code,
    string? Description,
    bool IsActive
     ) : IHttpRequest;