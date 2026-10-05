using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachinery;

public record CreateRequestMachineryCommand(
    long? Id,
    decimal TimeRequired,
    RequestMachineryUnit Unit,
    int RequestCount,
    DateTime FromDate,
    DateTime ToDate,
    string? Description,
    Project Project,
    Machinery Machinery,
    bool IsDeleted,
    long? CompanyId) : ICommand<RequestMachinery>;
