using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.ProjectOperationTemporaryDailyDocumentModel;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetProjectOperationTemporaryDailyById;

public record GetProjectOperationTemporaryDailyByIdResponse
{
    public long Id { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? CostCenterCode { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public long? ProjectOperationId { get; set; }
    public string? ProjectOperationName { get; set; } = string.Empty;
    public string? ProjectOperationCode { get; set; } = string.Empty;
    public TemporaryDailyStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public DateTime StartDate { get; set; }
    public string? StartDateShamsi => TimeCalculator.ConvertToShamsi(StartDate);
    public DateTime EndDate { get; set; }
    public string? EndDateShamsi => TimeCalculator.ConvertToShamsi(EndDate);
    public string? Description { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public List<ProjectOperationTemporaryDailyDocumentResponseModel>? Documents { get; set; } = new();
}
