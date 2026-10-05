
namespace Engineering.Application.Services.PublicGroups.Models.CreatePublicGroup;

public class CreatePublicGroupValidator : AbstractValidator<CreatePublicGroupRequest>
{
    public CreatePublicGroupValidator()
    {
        RuleFor(oo => oo.ProductGroupIds).NotEmpty().WithError(OperationInfoErrors.NonstandardIdsIsEmpty);
    }
}
