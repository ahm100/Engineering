using Engineering.Application.Services.OperationInfos.Models.OperationInfoActions;

namespace Engineering.Application.Services.OperationInfos.Models.CreateOperationInfoActions;

public class CreateOperationInfoActionValidator : AbstractValidator<CreateOperationInfoActionRequest>
{
    public CreateOperationInfoActionValidator()
    {
        RuleFor(oo => oo.OperationInfoId)
            .IsPositive(OperationInfoErrors.IdIsEmpty);
        RuleFor(oo => oo.ActionId)
            .IsPositive(OperationInfoErrors.IdIsEmpty);
    }
}
