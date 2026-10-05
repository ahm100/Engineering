using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using Engineering.Domain.Entities.Machineries;
using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.UpdateFixAssetMachinery;

public record UpdateFixAssetMachineryCommand(
    long Id,
    Machinery Machinery,
    string? MachinerySpecification,
    string? NumberPlates,
    FixAssetMachineryType FixAssetMachineryType,
    decimal? MachineryPrice,
    bool IsActive,
    long? ContractorId,
    long? DriverId,
    string? DriverName,
    DateTime? StartDate,
    DateTime? EndDate,
    string? Description,
    long? CompanyId
    ) : ICommand<FixAssetMachinery>;