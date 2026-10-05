using Engineering.Application.Services.RequestGoodsSupplies.Models.UpdateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSAdvertisementTypes.Update;

public record UpdateRGSAdvertisementTypesCommand(RequestGoodsSupply Entity,
    List<UpdateAdvertisementRGSTypeModel> AdvertisementModels) : ICommand<List<RequestGoodsSupplyType>?>;