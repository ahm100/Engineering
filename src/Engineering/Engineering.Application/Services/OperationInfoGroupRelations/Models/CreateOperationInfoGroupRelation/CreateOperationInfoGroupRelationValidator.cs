
namespace Engineering.Application.Services.OperationInfoGroupRelations.Models.CreateOperationInfoGroupRelation;

public class CreateOperationInfoGroupRelationValidator : AbstractValidator<CreateOperationInfoGroupRelationRequest>
{
    public CreateOperationInfoGroupRelationValidator()
    {
        RuleFor(oo => oo.OperationInfoId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoGroupRelationErrors.OperationInfoIsEmpty);
    }
}
