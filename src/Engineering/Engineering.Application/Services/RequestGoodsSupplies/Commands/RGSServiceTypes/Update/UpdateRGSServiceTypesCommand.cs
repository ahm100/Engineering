using Engineering.Application.Services.RequestGoodsSupplies.Models.UpdateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSServiceTypes.Update;

public record UpdateRGSServiceTypesCommand(RequestGoodsSupply Entity,
    List<UpdateServiceRGSTypeModel> ServiceModels) : ICommand<List<RequestGoodsSupplyType>?>;