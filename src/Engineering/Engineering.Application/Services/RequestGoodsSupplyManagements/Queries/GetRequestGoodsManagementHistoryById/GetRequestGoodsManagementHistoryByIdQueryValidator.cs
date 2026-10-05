
namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetRequestGoodsManagementHistoryById;

public class GetRequestGoodsManagementHistoryByIdQueryValidator : AbstractValidator<GetRequestGoodsManagementHistoryByIdQuery>
{
    public GetRequestGoodsManagementHistoryByIdQueryValidator()
    {
        RuleFor(c => c.Id).NotNull().WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
