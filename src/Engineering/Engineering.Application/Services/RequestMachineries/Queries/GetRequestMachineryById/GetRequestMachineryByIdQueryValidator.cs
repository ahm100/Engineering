namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryById;

public class GetRequestMachineryByIdQueryValidator : AbstractValidator<GetRequestMachineryByIdQuery>
{
    public GetRequestMachineryByIdQueryValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachineryId);
    }
}
