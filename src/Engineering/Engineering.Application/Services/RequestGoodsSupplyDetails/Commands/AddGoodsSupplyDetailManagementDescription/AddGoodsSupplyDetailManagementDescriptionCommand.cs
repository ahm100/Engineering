using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.AddGoodsSupplyDetailManagementDescription;

public record AddGoodsSupplyDetailManagementDescriptionCommand(
    long Id,
    string? ManagementDescription
    ) : ICommand<RequestGoodsSupplyDetail>;
