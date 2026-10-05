using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.CreateRequestGoodsSupply;

public record CreateRequestGoodsSupplyCommand(
    ProjectOperation ProjectOperation,
    ProjectOperationDetail? ProjectOperationDetail,
    OperationInfoSeason OperationInfoSeason,
    GoodsSupplyType Type,
    bool IsDraft,
    long? SupplyerId,
    long? BuyerId,
    long? CurrencyId,
    long? RequestingOrganizationId,
    decimal? TransferPrice,
    decimal? OtherPrice,
    decimal? DiscountOnInvoicePercentage,
    decimal? DiscountOnInvoiceNumber,
    decimal? DiscountedPriceOnInvoice,
    decimal? TaxOnInvoicePercentage,
    decimal? TaxOnInvoiceNumber,
    decimal? FinalInvoiceAmount,
    DateTime? RequestedDate,
    bool? IsPettyCash,
    string? Description,
    long? CompanyId,
    string? ConsumptionRateAndInventoryUrl,
    string? ConsumptionAddress,
    PurchaseLocation? PurchaseLocation,
    PurchaseReason? PurchaseReason
    ) : ICommand<RequestGoodsSupply>;
