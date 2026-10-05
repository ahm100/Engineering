
namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementById;

public record GetEmployerStatusStatementByIdRequest(
    long Id
     ) : IHttpRequest;
