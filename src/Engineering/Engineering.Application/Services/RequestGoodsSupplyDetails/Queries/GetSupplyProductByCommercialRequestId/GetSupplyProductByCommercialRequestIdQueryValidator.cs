
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetSupplyProductByCommercialRequestId;

public class GetSupplyProductByCommercialRequestIdQueryValidator : AbstractValidator<GetSupplyProductByCommercialRequestIdQuery>
{
    public GetSupplyProductByCommercialRequestIdQueryValidator()
    {
        RuleFor(c => c.CommercialRequestId)
            .IsPositive(GlobalCmts.Id);
    }
}
