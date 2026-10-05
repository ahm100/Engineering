namespace Engineering.Application.Services.RequestMachineries.Models.CreateRequestMachineryBillDocument;

public record CreateRequestMachineryBillDocumentRequest(
    long RequestMachineryId,
    List<string>? BillDocuments) : IHttpRequest;
