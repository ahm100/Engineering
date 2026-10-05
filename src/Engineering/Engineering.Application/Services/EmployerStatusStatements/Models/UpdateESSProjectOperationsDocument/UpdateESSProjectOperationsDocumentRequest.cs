
namespace Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationsDocument;

public record UpdateESSProjectOperationsDocumentRequest(
    List<UpdateESSProjectOperationsDocumentModel> ESSProjectOperations
     ) : IHttpRequest;

public record UpdateESSProjectOperationsDocumentModel(
    long Id,
    List<string>? Urls
     );
