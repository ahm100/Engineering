namespace Engineering.Application.Services.ProjectOperations.Models.GetPOActionByPOId;

public class GetPOActionByPOIdValidator : AbstractValidator<GetPOActionByPOIdRequest>
{
    public GetPOActionByPOIdValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).IsPositive(ProjectErrors.IdIsEmpty);
    }
}