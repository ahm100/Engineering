namespace Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupByName;

public class GetOperationInfoGroupByNameValidator : AbstractValidator<GetOperationInfoGroupByNameRequest>
{
    public GetOperationInfoGroupByNameValidator()
    {
        RuleFor(oo => oo.OperationInfoGroupName).NotEmpty().WithError(OperationInfoGroupErrors.OperationInfoGroupNameIsEmpty);
    }
}
