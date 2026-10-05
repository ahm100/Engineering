using Engineering.Domain.Entities.CostOvers;

namespace Engineering.Application.Services.CostOvers.Models.GetCostOverById;

public record GetCostOverByIdResponse
{
    public long Id { get; set; }
    public CostOverApprovalStatus ApprovalStatus { get; set; }
    public Guid? CurrentApprovalAttemptId { get; set; }
    public string CostOverName { get; set; } = string.Empty;
    public string CostOverCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
