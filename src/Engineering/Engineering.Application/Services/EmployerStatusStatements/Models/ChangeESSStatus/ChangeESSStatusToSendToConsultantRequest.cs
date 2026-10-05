using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationDetailDailyVolume;
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationsZeroVolume;

namespace Engineering.Application.Services.EmployerStatusStatements.Models.ChangeESSStatus;

public record ChangeESSStatusToSendToConsultantRequest(
    long Id,
    List<ESSProjectOperationZeroVolume>? ESSProjectOperations,
    List<ESSProjectOperationDetailDailyVolume>? ESSProjectOperationDetailDailies,
    string? Discription
     ) : IHttpRequest;
