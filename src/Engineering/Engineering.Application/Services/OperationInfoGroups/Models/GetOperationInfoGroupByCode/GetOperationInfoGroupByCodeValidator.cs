namespace Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupByCode;

public class GetOperationInfoGroupByCodeValidator : AbstractValidator<GetOperationInfoGroupByCodeRequest>
{
    public GetOperationInfoGroupByCodeValidator()
    {
        RuleFor(oo => oo.OperationInfoGroupCode).NotEmpty().WithError(OperationInfoGroupErrors.OperationInfoGroupCodeIsEmpty);
    }
}
