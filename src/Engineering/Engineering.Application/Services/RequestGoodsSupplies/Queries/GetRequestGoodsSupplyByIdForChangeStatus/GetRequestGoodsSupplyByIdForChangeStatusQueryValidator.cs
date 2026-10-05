
namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyByIdForChangeStatus;

public class GetRequestGoodsSupplyByIdForChangeStatusQueryValidator : AbstractValidator<GetRequestGoodsSupplyByIdForChangeStatusQuery>
{
    public GetRequestGoodsSupplyByIdForChangeStatusQueryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
