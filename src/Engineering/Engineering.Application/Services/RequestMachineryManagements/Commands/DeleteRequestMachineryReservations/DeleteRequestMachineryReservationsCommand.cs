using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryReservations;

public record DeleteRequestMachineryReservationsCommand(RequestMachinery RequestMachinery) : ICommand<bool?>;
