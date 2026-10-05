using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Commands.CreateMachinery;

public record CreateMachineryCommand(
    MachineriesGroup MachineriesGroup,
    string MachineryCode,
    string MachineryName,
    bool IsActive,
    long? CompanyId
    ) : ICommand<Machinery?>;