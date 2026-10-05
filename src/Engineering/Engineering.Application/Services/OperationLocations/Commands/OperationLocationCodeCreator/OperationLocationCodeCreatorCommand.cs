
namespace Engineering.Application.Services.OperationLocations.Commands.OperationLocationCodeCreator;

public record OperationLocationCodeCreatorCommand(
    long? CostCenterId,
    long? ProjectId,
    long? ParentId,
    long? CompanyId
    ) : ICommand<string?>;
