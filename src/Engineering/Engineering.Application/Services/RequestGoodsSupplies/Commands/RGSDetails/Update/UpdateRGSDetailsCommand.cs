using Engineering.Application.Services.RequestGoodsSupplies.Models.UpdateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSDetails.Update;

public record UpdateRGSDetailsCommand(RequestGoodsSupply Entity,
    List<UpdateRGSTypeDetailModel> Models) : ICommand<bool?>;