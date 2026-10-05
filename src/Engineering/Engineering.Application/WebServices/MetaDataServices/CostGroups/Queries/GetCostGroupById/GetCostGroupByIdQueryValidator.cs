
namespace Engineering.Application.WebServices.MetaDataServices.CostGroups.Queries.GetCostGroupById;

public class GetCostGroupByIdQueryValidator : AbstractValidator<GetCostGroupByIdQuery>
{
    public GetCostGroupByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}