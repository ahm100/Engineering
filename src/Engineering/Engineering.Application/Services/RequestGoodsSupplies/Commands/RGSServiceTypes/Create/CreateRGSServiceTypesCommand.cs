using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSServiceTypes.Create;

public record CreateRGSServiceTypesCommand(RequestGoodsSupply Entity,
    List<CreateRGSTypeServiceModel> TypeModels) : ICommand<List<RequestGoodsSupplyType>?>;