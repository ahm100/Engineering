using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachinery;

public record CreateFixAssetMachineryCommand(
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
    ) : ICommand<FixAssetMachinery?>;