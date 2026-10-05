
namespace Engineering.Application.Services.EmployerStatusStatements.Models.ChangeESSStatus;

public record ChangeESSStatusToInvalidatedRequest(
    long Id,
    string? Discription
     ) : IHttpRequest;
