using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateTemporaryRequestGoodsSupply;

public record UpdateTemporaryRequestGoodsSupplyDetailModel(
    long? ProjectOperationDetailId,
    long? ProductGroupId,
    long? ProductId,
    decimal? RequestedCount,
    DateTime? DelivaryDeadLine,
    GoodsSupplyDetailImportance? Importance,
    List<string>? DocumentUrls,
    long? CurrencyId,
    decimal? TotalPrice,
    string? Description,
    long? DestinationWarehouseId,
    long? PackageId,
    decimal? PackageCount
    );
