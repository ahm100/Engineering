
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProjectOperationDetailsByRequestId;

public record GetProjectOperationDetailsByRequestIdRequest(
    long ProjectOperationId,
    long ProductGroupId,
    decimal RequestedCount,
    DividerByProjectOperationDetailsType Type,
    long? ProjectOperationDetailId,
    List<long>? ProjectOperationDetailIds,
    string? FilterData,
    long? ContractorId,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
