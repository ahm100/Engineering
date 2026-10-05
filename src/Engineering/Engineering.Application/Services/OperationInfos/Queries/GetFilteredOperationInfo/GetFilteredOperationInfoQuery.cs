
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

namespace Engineering.Application.Services.OperationInfos.Queries.GetFilteredOperationInfo;

public record GetFilteredOperationInfoQuery(
    List<long>? Ids,
    string? FilterData,
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    bool? IsActive,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetOperationInfosModel>>>;