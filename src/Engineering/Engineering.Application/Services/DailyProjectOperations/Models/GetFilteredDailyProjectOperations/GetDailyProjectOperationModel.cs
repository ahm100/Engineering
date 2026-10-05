using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredDailyProjectOperations;

public record GetDailyProjectOperationModel
{
    public long ProjectOperationId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public decimal WorkLoad { get; set; }
    public decimal DoneAmount { get; set; }
    public decimal RemainingAmount => WorkLoad - DoneAmount;
    public string? UnitOfMeasurement { get; set; } = string.Empty;
    public ProjectOperationStatus? Status { get; set; }
    public string? StatusDescription => Status != null ? Status!.GetEnumDescription() : null;
    public string? Description { get; set; } = string.Empty;
}
