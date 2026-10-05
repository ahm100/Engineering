using Engineering.Domain.Entities.WorkflowRequests;

namespace Engineering.Application.Services.CostOvers.Commands.SubmitCostOverForApproval;

/// <summary>ارسال هزینه بالاسری در شرکت و هویت احرازشده.</summary>
public sealed record SubmitCostOverForApprovalCommand(
    long Id,
    long CompanyId,
    long UserId) : ICommand<WorkflowRequest>;
