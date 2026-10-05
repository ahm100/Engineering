using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProductTypes.Create;

public record CreateRGSProductTypesCommand(RequestGoodsSupply Entity,
    List<CreateRGSTypeProductModel> TypeModels,
    long ProjectId) : ICommand<List<RequestGoodsSupplyType>?>;