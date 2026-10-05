
namespace Engineering.Application.Services.CostCenterVirtualGroups.Queries.GetCreateCostCenterVirtualGroupById;

public class GetCreateCostCenterVirtualGroupByIdQueryValidator : AbstractValidator<GetCreateCostCenterVirtualGroupByIdQuery>
{
    public GetCreateCostCenterVirtualGroupByIdQueryValidator()
    {
        RuleFor(oo => oo.CostCenterVirtualGroupId).NotNull().WithError(CostCenterVirtualGroupErrors.InValidCostCenterVirtualGroup);
    }
}
