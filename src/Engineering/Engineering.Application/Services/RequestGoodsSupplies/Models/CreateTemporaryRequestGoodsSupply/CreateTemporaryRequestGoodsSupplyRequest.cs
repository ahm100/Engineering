using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.CreateTemporaryRequestGoodsSupply;

public record CreateTemporaryRequestGoodsSupplyRequest(
    long ProjectOperationId,
    long? ProjectOperationDetailId,
    GoodsSupplyType Type,
    string? Description,
    List<CreateTemporaryRequestGoodsSupplyDetailModel> Details
    ) : IHttpRequest;
