using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryConfirmDescription;

public record UpdateRequestMachineryConfirmDescriptionCommand(RequestMachinery RequestMachinery,
                                                       decimal? ConfirmTimeRequired,
                                                       string? ConfirmedDescription
                                                       ) : ICommand<RequestMachinery>;
