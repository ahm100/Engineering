namespace Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateById;

public record GetWbsTemplateByIdRequest(
    long Id) : IHttpRequest;