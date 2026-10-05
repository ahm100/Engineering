
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplyProductGroupStatusChanger;

public class RequestGoodsSupplyProductGroupStatusChangerValidator : AbstractValidator<RequestGoodsSupplyProductGroupStatusChangerRequest>
{
    public RequestGoodsSupplyProductGroupStatusChangerValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(GlobalCmts.Id);
        RuleFor(c => c.Status)
            .IsEnum(GlobalCmts.Status);
    }
}
