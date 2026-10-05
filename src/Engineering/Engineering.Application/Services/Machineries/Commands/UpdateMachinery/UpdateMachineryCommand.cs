using Engineering.Domain.Entities.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Commands.UpdateMachinery;

public record UpdateMachineryCommand(
    long Id,
    MachineriesGroup MachineriesGroup,
    string MachineryName,
    string MachineryCode,
    bool IsActive,
    long? CompanyId
    ) : ICommand<Machinery>;