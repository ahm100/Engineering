namespace Engineering.Application.Services.RequestContractors.Queries.GetRequestContractorByIds;

public class GetRequestContractorByIdsQueryValidator : AbstractValidator<GetRequestContractorByIdsQuery>
{
    public GetRequestContractorByIdsQueryValidator()
    {
        RuleFor(oo => oo.RequestContractorIds).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidIds);
    }
}
