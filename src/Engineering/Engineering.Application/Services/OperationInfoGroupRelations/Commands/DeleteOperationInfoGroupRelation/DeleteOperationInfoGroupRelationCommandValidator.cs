namespace Engineering.Application.Services.OperationInfoGroupRelations.Commands.DeleteOperationInfoGroupRelation;

public class DeleteOperationInfoGroupRelationCommandValidator : AbstractValidator<DeleteOperationInfoGroupRelationCommand>
{
    public DeleteOperationInfoGroupRelationCommandValidator()
    {
        RuleFor(oo => oo.OprationInfoId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoGroupRelationErrors.OperationInfoIsEmpty);
        RuleFor(oo => oo.OperationInfoGroupId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoGroupRelationErrors.IdIsEmpty);
    }
}
