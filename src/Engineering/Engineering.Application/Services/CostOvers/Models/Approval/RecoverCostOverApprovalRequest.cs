namespace Engineering.Application.Services.CostOvers.Models.Approval;

/// <summary>شناسه موجودیت و تلاش پایان یافته برای جلوگیری از بازیابی تلاش اشتباه.</summary>
public sealed record RecoverCostOverApprovalRequest(long Id, Guid RequestId);