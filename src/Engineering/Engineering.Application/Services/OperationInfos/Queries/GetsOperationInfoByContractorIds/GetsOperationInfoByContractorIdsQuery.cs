
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByContractorIds;

public record GetsOperationInfoByContractorIdsQuery(
    List<long> ContractorIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfo>>>;
