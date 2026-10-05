
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationDetailDailyVolume;
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationsZeroVolume;

namespace Engineering.Application.Services.EmployerStatusStatements.Models.ChangeESSStatus;

public record ChangeESSStatusRequest(
    long Id,
    Domain.Entities.EmployerStatusStatements.Enums.EmployerStatusStatementStatus Status,
    List<ESSProjectOperationZeroVolume>? ESSProjectOperations,
    List<ESSProjectOperationDetailDailyVolume>? ESSProjectOperationDetailDailies,
    string? Description
     ) : IHttpRequest;
