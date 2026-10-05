using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSDetails.Create;

public record CreateRGSDetailsCommand(RequestGoodsSupply Entity,
    List<RequestGoodsSupplyType> Types,
    List<CreateRGSTypeDetailModel> DetailModels) : ICommand<List<RequestGoodsSupplyTypeDetail>?>;