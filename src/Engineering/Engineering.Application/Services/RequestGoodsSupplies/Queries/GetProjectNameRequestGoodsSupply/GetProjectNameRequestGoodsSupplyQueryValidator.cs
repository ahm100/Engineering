namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetProjectNameRequestGoodsSupply;

public class GetProjectNameRequestGoodsSupplyQueryValidator : AbstractValidator<GetProjectNameRequestGoodsSupplyQuery>
{
    public GetProjectNameRequestGoodsSupplyQueryValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
