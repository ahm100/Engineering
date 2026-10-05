using Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRGSType;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.DeleteRGSType;

public record DeleteRGSTypeCommand(long Id) : ICommand<DeleteRGSTypeResponse?>;