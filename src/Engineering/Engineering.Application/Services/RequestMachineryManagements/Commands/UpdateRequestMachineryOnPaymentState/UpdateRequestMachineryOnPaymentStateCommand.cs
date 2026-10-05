using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.UpdateRequestMachineryOnPaymentState;

public record UpdateRequestMachineryOnPaymentStateCommand(
    List<long> RequestMachineriesId,
    RequestMachineryStatus Status,
    string? Description) : ICommand<bool>;
