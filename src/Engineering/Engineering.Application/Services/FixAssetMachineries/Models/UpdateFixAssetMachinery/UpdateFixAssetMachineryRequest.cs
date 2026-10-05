using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.FixAssetMachineries.Models.UpdateFixAssetMachinery;

public record UpdateFixAssetMachineryRequest(
    long Id,
    long MachineryId,
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
    List<string>? Documents,
    List<UpdateFixAssetRateRequest> Rates
     ) : IHttpRequest;

public record UpdateFixAssetRateRequest(
    long? Id,
    DateTime StartDate,
    DateTime EndDate,
    TimeSpan? StartTime,
    TimeSpan? EndTime,
    decimal? HourlyRate,
    decimal? DailyRate,
    decimal? ServiceRate,
    decimal? VolumeRate,
    bool IsDeleted
);