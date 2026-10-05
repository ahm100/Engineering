using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.CreateRGSType;

public record CreateRGSTypeCommand(
    CreateRGSTypeRequest Request) : ICommand<CreateRGSTypeResponse?>;