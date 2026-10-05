using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationsZeroVolume;

public record UpdateESSProjectOperationsZeroVolumeCommand(
    EmployerStatusStatementProjectOperation Entity,
    List<string>? Urls,
    string? Description
    ) : ICommand<EmployerStatusStatementProjectOperation>;
