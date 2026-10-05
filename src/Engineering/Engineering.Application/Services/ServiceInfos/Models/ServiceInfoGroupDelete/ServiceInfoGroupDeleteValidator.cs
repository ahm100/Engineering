
namespace Engineering.Application.Services.ServiceInfos.Models.ServiceInfoGroupDelete;

public class ServiceInfoGroupDeleteValidator : AbstractValidator<ServiceInfoGroupDeleteRequest>
{
    public ServiceInfoGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
