namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfoByCode;

public class GetOperationInfoByCodeValidator : AbstractValidator<GetOperationInfoByCodeRequest>
{
    public GetOperationInfoByCodeValidator()
    {
        RuleFor(oo => oo.OperationInfoCode).NotEmpty().WithError(OperationInfoErrors.OperationInfoCodeIsEmpty);
    }
}
