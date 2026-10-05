
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetDeductionAmountByProjectOperationDetailId;

public record GetDeductionAmountByProjectOperationDetailIdResponse(
     long ProjectOperationDetailId,
     decimal DeductionFinalAmounts
    );
