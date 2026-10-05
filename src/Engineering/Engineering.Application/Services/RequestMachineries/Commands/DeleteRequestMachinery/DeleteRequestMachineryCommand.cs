using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachinery;

public record DeleteRequestMachineryCommand(long RequestMachineryId) : ICommand<RequestMachinery>;
