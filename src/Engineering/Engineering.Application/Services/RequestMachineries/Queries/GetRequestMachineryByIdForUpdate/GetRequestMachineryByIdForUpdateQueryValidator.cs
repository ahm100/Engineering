namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryByIdForUpdate;

public class GetRequestMachineryByIdForUpdateQueryValidator : AbstractValidator<GetRequestMachineryByIdForUpdateQuery>
{
    public GetRequestMachineryByIdForUpdateQueryValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachineryId);
    }
}
