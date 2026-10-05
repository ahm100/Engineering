
namespace Engineering.Application.Services.Trips.Commands.CodeCreator;

public record TripCodeCreatorCommand(
    long? CompanyId
    ) : ICommand<string?>;
