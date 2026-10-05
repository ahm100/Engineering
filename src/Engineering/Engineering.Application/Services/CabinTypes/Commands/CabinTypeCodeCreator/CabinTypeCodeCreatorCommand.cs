namespace Engineering.Application.Services.CabinTypes.Commands.CabinTypeCodeCreator;

public record CabinTypeCodeCreatorCommand(
    long? CompanyId)
    : ICommand<int?>;