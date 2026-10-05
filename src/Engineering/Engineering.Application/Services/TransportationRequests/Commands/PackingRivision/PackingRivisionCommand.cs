namespace Engineering.Application.Services.TransportationRequests.Commands.PackingRivision;

public record PackingRivisionCommand(
    List<long>? Ids,
    List<long>? PackingIds,
    string? Description
    ) : ICommand<bool?>;