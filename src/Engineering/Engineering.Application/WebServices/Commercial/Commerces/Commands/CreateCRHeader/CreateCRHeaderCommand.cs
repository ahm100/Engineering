using Engineering.Application.WebServices.Commercial.Commerces.Models.CreateCRHeader;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.WebServices.Commercial.Commerces.Commands.CreateCRHeader;

public class CreateCRHeaderCommand : ICommand<CreateCRHeaderResponse?>
{
    public required RequestGoodsSupply RequestGoodsSupply { get; set; }
    public List<CreateCRCommand>? Details { get; set; }
}
public class CreateCRCommand
{
    public required RequestGoodsSupplyType RequestGoodsSupplyType { get; set; }
    public List<RequestGoodsSupplyTypeDetail>? RequestGoodsSupplyTypeDetails { get; set; }
}