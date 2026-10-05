namespace Engineering.Application.Services.OperationInfos.Models.GetOIActionByOperationInfoId;

public class GetOIActionByOperationInfoIdValidator : AbstractValidator<GetOIActionByOperationInfoIdRequest>
{
    public GetOIActionByOperationInfoIdValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(OperationInfoErrors.IdIsEmpty);
    }
}
