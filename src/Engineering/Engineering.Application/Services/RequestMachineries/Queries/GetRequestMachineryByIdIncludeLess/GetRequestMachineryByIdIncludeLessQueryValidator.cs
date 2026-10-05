namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryByIdIncludeLess;

public class GetRequestMachineryByIdIncludeLessQueryValidator : AbstractValidator<GetRequestMachineryByIdIncludeLessQuery>
{
    public GetRequestMachineryByIdIncludeLessQueryValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachineryId);
    }
}
