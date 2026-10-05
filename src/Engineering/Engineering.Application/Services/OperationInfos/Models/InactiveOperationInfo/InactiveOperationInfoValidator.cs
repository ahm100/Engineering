namespace Engineering.Application.Services.OperationInfos.Models.InactiveOperationInfo;

public class InactiveOperationInfoValidator : AbstractValidator<InactiveOperationInfoRequest>
{
    public InactiveOperationInfoValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}
