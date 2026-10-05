using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Commands.ChangeRequestMachineryStatus;

public record ChangeRequestMachineryStatusCommand(
    RequestMachinery Entity,
    RequestMachineryStatus Status,
    string? Description,
    long? ContractorId
    ) : ICommand<RequestMachinery>;
