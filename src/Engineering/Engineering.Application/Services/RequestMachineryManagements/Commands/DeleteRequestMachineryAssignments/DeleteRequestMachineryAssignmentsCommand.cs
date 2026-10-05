using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryAssignments;

public record DeleteRequestMachineryAssignmentsCommand(RequestMachinery RequestMachinery) : ICommand<bool?>;
