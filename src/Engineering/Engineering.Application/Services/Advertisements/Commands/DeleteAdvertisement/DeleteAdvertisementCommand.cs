using Engineering.Application.Services.Advertisements.Contracts.DeleteAdvertisement;

namespace Engineering.Application.Services.Advertisements.Commands.DeleteAdvertisement;

public record DeleteAdvertisementCommand(
    long Id) : ICommand<DeleteAdvertisementResponse?>;