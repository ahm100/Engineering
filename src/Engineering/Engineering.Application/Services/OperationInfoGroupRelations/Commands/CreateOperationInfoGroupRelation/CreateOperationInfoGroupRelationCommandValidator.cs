namespace Engineering.Application.Services.OperationInfoGroupRelations.Commands.CreateOperationInfoGroupRelation;

public class CreateOperationInfoGroupRelationCommandValidator : AbstractValidator<CreateOperationInfoGroupRelationCommand>
{
    public CreateOperationInfoGroupRelationCommandValidator()
    {
        RuleFor(oo => oo.OperationInfo).NotEmpty().WithError(OperationInfoGroupRelationErrors.OperationInfoIsEmpty);
    }
}
