using Engineering.Application.Services.Seasons.Models.GetsByBranchIdWhithOperationInfo;

namespace Engineering.Application.Services.Seasons.Queries.GetsByBranchIdWhithOperationInfo;

public record GetsByBranchIdWhithOperationInfoQuery(
    long BranchId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsByBranchIdWhithOperationInfoModel>>>;