using Engineering.Domain.Entities.Projects;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Commands.UpdateOperationLocation;

public record UpdateOperationLocationCommand(
    long Id,
    OperationLocation? Parent,
    Project? Project,
    string PrivateName,
    string PrivateCode,
    string PublicName,
    string PublicCode,
    int? Priority,
    bool IsActive,
    long? CompanyId
    ) : ICommand<OperationLocation>;