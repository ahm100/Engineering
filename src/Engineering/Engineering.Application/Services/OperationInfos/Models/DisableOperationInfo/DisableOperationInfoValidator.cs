namespace Engineering.Application.Services.OperationInfos.Models.DisableOperationInfo;

public class DisableOperationInfoValidator : AbstractValidator<DisableOperationInfoRequest>
{
    public DisableOperationInfoValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoErrors.IdIsEmpty);
    }
}
