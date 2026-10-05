namespace Engineering.Application.Services.OperationInfos.Models.ActiveOperationInfo;

public class ActiveOperationInfoValidator : AbstractValidator<ActiveOperationInfoRequest>
{
    public ActiveOperationInfoValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoErrors.IdIsEmpty);
    }
}
