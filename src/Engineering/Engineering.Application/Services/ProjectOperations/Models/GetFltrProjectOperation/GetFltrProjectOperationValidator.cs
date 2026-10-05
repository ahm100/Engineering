namespace Engineering.Application.Services.ProjectOperations.Models.GetFltrProjectOperation;

public class GetFltrProjectOperationValidator : AbstractValidator<GetFltrProjectOperationRequest>
{
    public GetFltrProjectOperationValidator()
    {
        RuleFor(oo => oo.CostCenterId).IsPositive(ProjectOperationErrors.IdIsEmpty);
    }
}