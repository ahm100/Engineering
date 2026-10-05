using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSAdvertisementTypes.Create;

public record CreateRGSAdvertisementTypesCommand(RequestGoodsSupply Entity,
    List<CreateRGSTypeAdvertisementModel> TypeModels) : ICommand<List<RequestGoodsSupplyType>?>;