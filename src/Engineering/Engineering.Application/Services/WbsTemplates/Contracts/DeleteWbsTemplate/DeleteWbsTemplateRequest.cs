namespace Engineering.Application.Services.WbsTemplates.Contracts.DeleteWbsTemplate;

public record DeleteWbsTemplateRequest(
    long Id) : IHttpRequest;