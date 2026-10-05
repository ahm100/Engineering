namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfoByName;

public class GetOperationInfoByNameValidator : AbstractValidator<GetOperationInfoByNameRequest>
{
    public GetOperationInfoByNameValidator()
    {
        RuleFor(oo => oo.OperationInfoName).NotEmpty().WithError(OperationInfoErrors.OperationInfoNameIsEmpty);
    }
}
