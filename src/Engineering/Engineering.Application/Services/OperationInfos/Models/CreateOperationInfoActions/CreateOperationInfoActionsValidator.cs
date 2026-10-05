namespace Engineering.Application.Services.OperationInfos.Models.CreateOperationInfoActions;

public class CreateOperationInfoActionsValidator : AbstractValidator<CreateOperationInfoActionsRequest>
{
    public CreateOperationInfoActionsValidator()
    {
        RuleFor(oo => oo.OperationInfoId)
            .IsPositive(OperationInfoCmts.Id);
    }
}