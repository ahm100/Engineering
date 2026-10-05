namespace Engineering.Application.Services.RequestMachineries.Models.CreateRequestMachineryDocument;

public record CreateRequestMachineryDocumentRequest(
    long RequestMachineryId,
    List<string>? Documents) : IHttpRequest;
