using Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRGS;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.DeleteRGS;

public record DeleteRGSCommand(
    long Id) : ICommand<DeleteRGSResponse?>;