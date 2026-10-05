namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfoById;

public class GetOperationInfoByIdValidator : AbstractValidator<GetOperationInfoByIdRequest>
{
    public GetOperationInfoByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}
