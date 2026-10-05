
namespace Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationDetailDailiesDocument;

public record UpdateESSProjectOperationDetailDailiesDocumentRequest(
    List<UpdateESSProjectOperationDetailDailiesDocumentModel> ESSProjectOperationDailies
     ) : IHttpRequest;

public record UpdateESSProjectOperationDetailDailiesDocumentModel(
    long Id,
    List<string>? Urls
     );
