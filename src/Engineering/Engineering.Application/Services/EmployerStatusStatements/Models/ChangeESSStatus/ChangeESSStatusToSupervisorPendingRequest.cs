
namespace Engineering.Application.Services.EmployerStatusStatements.Models.ChangeESSStatus;

public record ChangeESSStatusToSupervisorPendingRequest(
    long Id
     ) : IHttpRequest;
