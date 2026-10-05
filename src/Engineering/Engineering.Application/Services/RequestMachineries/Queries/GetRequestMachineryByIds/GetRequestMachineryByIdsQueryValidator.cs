namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryByIds;

public class GetRequestMachineryByIdsQueryValidator : AbstractValidator<GetRequestMachineryByIdsQuery>
{
    public GetRequestMachineryByIdsQueryValidator()
    {
        RuleFor(oo => oo.RequestMachineryIds).NotEmpty().WithError(RequestMachineryErrors.InValidRequestMachineryIds);
    }
}
