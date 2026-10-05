namespace Engineering.Application.Services.RequestContractors.Models.RequestContractorGroupDelete;

public class RequestContractorGroupDeleteValidator : AbstractValidator<RequestContractorGroupDeleteRequest>
{
    public RequestContractorGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
