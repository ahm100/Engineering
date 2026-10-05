using Engineering.Application.Services.RequestGoodsSupplies.Models.UpdateRGSType;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.UpdateRGSType;

public record UpdateRgsTypeCommand(
    UpdateRGSTypeRequest Request) : ICommand<UpdateRGSTypeResponse?>;