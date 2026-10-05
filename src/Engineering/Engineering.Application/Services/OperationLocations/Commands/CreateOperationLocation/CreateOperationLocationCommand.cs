using Engineering.Domain.Entities.Projects;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Commands.CreateOperationLocation;

public record CreateOperationLocationCommand(
    CostCenter? CostCenter,
    Project? Project,
    OperationLocation? Parent,
    string PrivateName,
    string PrivateCode,
    string PublicName,
    string PublicCode,
    int? Priority,
    bool IsActive,
    long? CompanyId
    ) : ICommand<OperationLocation?>;