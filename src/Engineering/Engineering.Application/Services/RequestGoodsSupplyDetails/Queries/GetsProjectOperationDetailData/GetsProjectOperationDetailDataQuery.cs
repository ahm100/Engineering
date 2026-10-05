using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsProjectOperationDetailData;

public record GetsProjectOperationDetailDataQuery(
    long ProjectOperationId,
    VolumeProductType Type,
    long ProductGroupId,
    long? ProjectOperationDetailId,
    string? FilterData,
    long? ContractorId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;

