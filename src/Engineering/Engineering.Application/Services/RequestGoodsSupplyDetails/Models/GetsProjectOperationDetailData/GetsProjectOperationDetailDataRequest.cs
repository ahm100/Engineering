using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsProjectOperationDetailData;

public record GetsProjectOperationDetailDataRequest(
    long ProjectOperationId,
    VolumeProductType Type,
    long ProductGroupId,
    long? ProjectOperationDetailId,
    string? FilterData,
    long? ContractorId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;