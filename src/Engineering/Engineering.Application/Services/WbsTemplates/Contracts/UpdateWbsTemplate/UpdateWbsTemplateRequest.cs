namespace Engineering.Application.Services.WbsTemplates.Contracts.UpdateWbsTemplate;

public record UpdateWbsTemplateRequest(
    long Id,
    string? Title,
    string? Code,
    string? Description,
    bool? IsActive
     ) : IHttpRequest;