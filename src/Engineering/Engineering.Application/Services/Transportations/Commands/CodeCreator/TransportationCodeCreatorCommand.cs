
namespace Engineering.Application.Services.Transportations.Commands.CodeCreator;

public record TransportationCodeCreatorCommand(
    long? CompanyId
    ) : ICommand<string?>;
