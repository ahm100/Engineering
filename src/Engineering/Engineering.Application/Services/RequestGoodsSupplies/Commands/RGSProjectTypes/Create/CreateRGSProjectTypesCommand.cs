using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProjectTypes.Create;

public record CreateRGSProjectTypesCommand(RequestGoodsSupply Entity,
    List<CreateRGSTypeProjectModel> TypeModels) : ICommand<List<RequestGoodsSupplyType>?>;