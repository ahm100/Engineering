namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfoAction;

public class GetOperationInfoActionsValidator : AbstractValidator<GetOperationInfoActionsRequest>
{
    public GetOperationInfoActionsValidator()
    {
        RuleFor(oo => oo.OInfoId)
            .IsPositive(OperationInfoErrors.IdIsEmpty);
    }
}
