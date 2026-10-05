
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetTotalSupplyByProjectOperationDetailIds;

public class GetTotalSupplyByProjectOperationDetailIdsQueryValidator : AbstractValidator<GetTotalSupplyByProjectOperationDetailIdsQuery>
{
    public GetTotalSupplyByProjectOperationDetailIdsQueryValidator()
    {
        RuleFor(c => c.ProjectOperationDetailIds).NotEmpty().WithError(RequestGoodsSupplyErrors.InValidProjectOperationDetail);
    }
}
