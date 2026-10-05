namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetDeductionAmountByProjectOperationDetailId;

public record GetDeductionAmountByProjectOperationDetailIdQuery(
    long ProjectOperationDetailId
    ) : IQuery<List<decimal>>;