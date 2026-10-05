namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetRequestGoodsSupplyManagementById;

public class GetRequestGoodsSupplyManagementByIdQueryValidator : AbstractValidator<GetRequestGoodsSupplyManagementByIdQuery>
{
    public GetRequestGoodsSupplyManagementByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(RequestGoodsSupplyManagementErrors.InValidRequestGoodsSupplyManagementId);
    }
}
