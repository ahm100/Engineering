namespace Engineering.Application.Services.RequestMachineries.Models.RequestMachineryGroupDelete;

public class RequestMachineryGroupDeleteValidator : AbstractValidator<RequestMachineryGroupDeleteRequest>
{
    public RequestMachineryGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
