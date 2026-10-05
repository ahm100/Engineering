
namespace Engineering.Application.Services.EmployerStatusStatements.Models.ChangeESSStatus;

public record ChangeESSStatusToPendingRequest(
    long Id
     ) : IHttpRequest;
