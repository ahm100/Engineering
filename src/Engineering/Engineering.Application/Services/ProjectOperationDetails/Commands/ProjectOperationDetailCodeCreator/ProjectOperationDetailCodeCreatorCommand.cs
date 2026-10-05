
namespace Engineering.Application.Services.ProjectOperationDetails.Commands.ProjectOperationDetailCodeCreator;

public record ProjectOperationDetailCodeCreatorCommand(
    long? ProjectOperationId,
    long? OperationLocationId,
    long? CompanyId
    ) : ICommand<string?>;