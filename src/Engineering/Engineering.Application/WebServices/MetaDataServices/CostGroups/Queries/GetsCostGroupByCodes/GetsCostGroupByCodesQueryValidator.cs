
namespace Engineering.Application.WebServices.MetaDataServices.CostGroups.Queries.GetsCostGroupByCodes;

public class GetsCostGroupByCodesQueryValidator : AbstractValidator<GetsCostGroupByCodesQuery>
{
    public GetsCostGroupByCodesQueryValidator()
    {
        RuleFor(oo => oo.Codes).NotEmpty().WithError(MetaDataErrors.CodeIsEmpty);
    }
}
