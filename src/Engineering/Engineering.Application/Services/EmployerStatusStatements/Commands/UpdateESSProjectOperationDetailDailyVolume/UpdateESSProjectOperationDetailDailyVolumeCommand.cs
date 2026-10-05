using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationDetailDailyVolume;

public record UpdateESSProjectOperationDetailDailyVolumeCommand(
    EmployerStatusStatementProjectOperationDetailDaily Entity,
    List<string>? Urls,
    decimal Length,
    decimal Width,
    decimal Height,
    decimal Weight,
    decimal Number,
    string? Description
    ) : ICommand<EmployerStatusStatementProjectOperationDetailDaily>;
