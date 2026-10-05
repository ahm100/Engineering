using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryMachinery;

public record UpdateRequestMachineryMachineryCommand(long RequestMachineryId,
                                                     Machinery Machinery) : ICommand<RequestMachinery>;
