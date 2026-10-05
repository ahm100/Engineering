namespace Engineering.Application.Services.PublicGroups.Commands.CreatePublicGroup;

public class CreatePublicGroupCommandValidator : AbstractValidator<CreatePublicGroupCommand>
{
    public CreatePublicGroupCommandValidator()
    {
        RuleFor(oo => oo.ProductGroupIds).NotEmpty().WithError(OperationInfoErrors.NonstandardIdsIsEmpty);
    }
}
