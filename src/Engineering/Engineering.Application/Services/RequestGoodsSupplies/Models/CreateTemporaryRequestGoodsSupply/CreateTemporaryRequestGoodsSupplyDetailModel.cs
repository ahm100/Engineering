using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.CreateTemporaryRequestGoodsSupply;

public record CreateTemporaryRequestGoodsSupplyDetailModel(
    long? ProjectOperationDetailId,
    long? ProductId,
    long? ProductGroupId,
    decimal? RequestedCount,
    DateTime? DelivaryDeadLine,
    GoodsSupplyDetailImportance? Importance,
    List<string>? DocumentUrls,
    long? CurrencyId,
    bool? CheckGroup,
    decimal? TotalPrice,
    string? Description,
    string? ManagementDescription,
    long? ContractorId,
    long? PackageId,
    decimal? PackageCount,
    long? DestinationWarehouseId
    );