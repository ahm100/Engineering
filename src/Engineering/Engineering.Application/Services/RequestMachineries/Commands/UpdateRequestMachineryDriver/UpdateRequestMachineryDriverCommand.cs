using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryDriver;

public record UpdateRequestMachineryDriverCommand(RequestMachinery RequestMachinery,
                                                       long? DriverId,
                                                       string? DriverName
                                                       ) : ICommand<RequestMachinery>;
