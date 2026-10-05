using Engineering.Application.Services.RequestGoodsSupplies.Models.UpdateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProjectTypes.Update;

public record UpdateRGSProjectTypesCommand(RequestGoodsSupply Entity,
    List<UpdateProjectRGSTypeModel> ProjectModels) : ICommand<List<RequestGoodsSupplyType>?>;