namespace Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationCodeWithParent;

public class CreateOperationLocationCodeWithParentValidator : AbstractValidator<CreateOperationLocationCodeWithParentRequest>
{
    public CreateOperationLocationCodeWithParentValidator()
    {
        RuleFor(oo => oo.ParentId).NotNull().WithError(OperationLocationErrors.ParentIdIsEmpty);
    }
}
