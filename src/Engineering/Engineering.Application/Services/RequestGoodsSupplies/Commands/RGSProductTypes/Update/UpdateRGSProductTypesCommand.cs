using Engineering.Application.Services.RequestGoodsSupplies.Models.UpdateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProductTypes.Update;

public record UpdateRGSProductTypesCommand(RequestGoodsSupply Entity,
    List<UpdateProductRGSTypeModel> ProductModels) : ICommand<List<RequestGoodsSupplyType>?>;