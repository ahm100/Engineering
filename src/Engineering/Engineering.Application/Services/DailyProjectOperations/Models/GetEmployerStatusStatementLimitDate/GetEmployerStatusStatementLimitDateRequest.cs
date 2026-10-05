
namespace Engineering.Application.Services.DailyProjectOperations.Models.GetEmployerStatusStatementLimitDate;

public record GetEmployerStatusStatementLimitDateRequest(
    long ProjectId,
    long EmployerContractId
     ) : IHttpRequest;
