using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservations;

public record GetMachineryReservationsResponse(
    List<GetMachineryReservationsModel> Data,
    int RowCount);

public record GetMachineryReservationsModel
{
    public long Id { get; set; }
    public long? MachineryId { get; set; }
    public string? MachineryName { get; set; } = string.Empty;
    public string? MachineryCode { get; set; } = string.Empty;
    public long? RequestMachineryId { get; set; }
    public string? RequestMachineryNumber => RequestMachineryId.ToString();
    public long? CostCenterId { get; set; }
    public string? CostCenter { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? Project { get; set; } = string.Empty;
    public long? FixAssetMachineryId { get; set; }
    public FixAssetMachineryType? FixAssetMachineryType { get; set; }
    public string? TypeDesctiption => FixAssetMachineryType?.GetEnumDescription();
    public MachineryReservationUnit MachineryReservationUnit { get; set; }
    public string? UnitDesctiption => MachineryReservationUnit.GetEnumDescription();
    public DateTime StartDate { get; set; }
    public string? StartDateShamsi => TimeCalculator.ConvertToShamsi(StartDate);
    public TimeSpan? StartTime { get; set; }
    public DateTime EndDate { get; set; }
    public string? EndDateShamsi => TimeCalculator.ConvertToShamsi(EndDate);
    public TimeSpan? EndTime { get; set; }
    public string? Description { get; set; } = string.Empty;
}

