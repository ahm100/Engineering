
namespace Engineering.Application.Services.EmployerStatusStatements.Models.UpdateEmployerStatusStatementDocuments;

public record UpdateEmployerStatusStatementDocumentsRequest(
    long Id,
    List<string>? Urls
     ) : IHttpRequest;
