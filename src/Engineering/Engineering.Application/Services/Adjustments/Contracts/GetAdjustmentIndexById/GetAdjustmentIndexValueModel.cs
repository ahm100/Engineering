using Engineering.Domain.Entities.Adjustment.Enums;
using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexById;


public class GetAdjustmentIndexValueModel
{
    public long Id { get; set; }
    public string YearName { get; set; } = string.Empty;
    public ContractAdjustmentPeriod Period { get; set; }
    public AdjustmentIndexType Type { get; set; }
    public decimal Value { get; set; }
    public decimal? Coefficient { get; set; }
    public string? NotificationNumber { get; set; }
    public DateTime? NotificationDate { get; set; }
    public bool IsActive { get; set; }
}