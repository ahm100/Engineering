using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationDetailDailyVolume;
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationsZeroVolume;

namespace Engineering.Application.Services.EmployerStatusStatements.Models.ChangeESSStatus;

public record ChangeESSStatusToSendToSupervisorRequest(
    long Id,
    List<ESSProjectOperationZeroVolume>? ESSProjectOperations,
    List<ESSProjectOperationDetailDailyVolume>? ESSProjectOperationDetailDailies,
    string? Discription
     ) : IHttpRequest;
