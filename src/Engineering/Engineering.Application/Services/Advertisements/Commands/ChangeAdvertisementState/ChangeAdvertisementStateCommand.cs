using Engineering.Application.Services.Advertisements.Contracts.ChangeAdvertisementState;

namespace Engineering.Application.Services.Advertisements.Commands.ChangeAdvertisementState;

public record ChangeAdvertisementStateCommand(
    List<long> Ids,
    bool IsActive
    ) : ICommand<ChangeAdvertisementStateResponse?>;