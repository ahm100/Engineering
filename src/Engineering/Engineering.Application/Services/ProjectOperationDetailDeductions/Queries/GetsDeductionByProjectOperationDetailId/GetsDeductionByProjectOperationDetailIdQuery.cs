using ProjectOperationDetailDeduction = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailDeduction;

namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetsDeductionByProjectOperationDetailId;

public record GetsDeductionByProjectOperationDetailIdQuery(
    long ProjectOperationDetailId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperationDetailDeduction>>>;