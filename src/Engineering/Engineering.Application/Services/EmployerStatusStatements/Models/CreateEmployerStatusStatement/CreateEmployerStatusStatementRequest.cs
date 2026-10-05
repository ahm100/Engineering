
namespace Engineering.Application.Services.EmployerStatusStatements.Models.CreateEmployerStatusStatement;

public record CreateEmployerStatusStatementRequest(
    long EmployerContractId,
    long ProjectId,
    long? LastEmployerStatusStatementId,
    DateTime StartDate,
    DateTime EndDate,
    List<string>? Urls,
    string? Description,
    List<CreateEmployerStatusStatementProjectOperation> ProjectOperations
     ) : IHttpRequest;

public record CreateEmployerStatusStatementProjectOperation(
    long Id,
    decimal? UnitConfirmePrice
     ) : IHttpRequest;
