namespace Engineering.Application.Services.OperationInfos.Models.DeleteOperationInfoAction;

public class DeleteOperationInfoActionValidator : AbstractValidator<DeleteOperationInfoActionRequest>
{
    public DeleteOperationInfoActionValidator()
    {
        RuleFor(oo => oo.OperationInfoId)
            .IsPositive(OperationInfoCmts.Id);

        RuleFor(oo => oo.ActionId)
            .IsPositive(GlobalCmts.ActionId);
    }
}
