
namespace Engineering.Application.Services.RequestGoodsSupplies.Models.RequestGoodsSuppliesGroupDelete;

public class RequestGoodsSuppliesGroupDeleteValidator : AbstractValidator<RequestGoodsSuppliesGroupDeleteRequest>
{
    public RequestGoodsSuppliesGroupDeleteValidator()
    {
        RuleForEach(c => c.Ids).IsPositive(GlobalCmts.Id);
    }
}
