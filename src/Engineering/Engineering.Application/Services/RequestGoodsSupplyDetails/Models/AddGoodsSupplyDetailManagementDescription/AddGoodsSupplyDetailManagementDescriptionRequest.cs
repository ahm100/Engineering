
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.AddGoodsSupplyDetailManagementDescription;

public record AddGoodsSupplyDetailManagementDescriptionRequest(
    long Id,
    string? ManagementDescription
    ) : IHttpRequest;
