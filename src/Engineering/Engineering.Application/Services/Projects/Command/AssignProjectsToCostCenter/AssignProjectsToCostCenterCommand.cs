using Engineering.Application.Services.Projects.Models.AssignProjectsToCostCenter;

namespace Engineering.Application.Services.Projects.Command.AssignProjectsToCostCenter;

public record AssignProjectsToCostCenterCommand(
    long CostCenterId,
    List<long> ProjectIds
     ) : ICommand<AssignProjectsToCostCenterResponse?>;