using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationDetailDailyVolume;
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationsZeroVolume;
using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements.Enums;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.ChangeEmployerStatusStatementStatus;

public record ChangeEmployerStatusStatementStatusCommand(
    long Id,
    EmployerStatusStatementStatus Status,
    List<ESSProjectOperationZeroVolume>? ESSProjectOperations,
    List<ESSProjectOperationDetailDailyVolume>? ESSProjectOperationDetailDailies,
    string? Description
    ) : ICommand<EmployerStatusStatement>;
