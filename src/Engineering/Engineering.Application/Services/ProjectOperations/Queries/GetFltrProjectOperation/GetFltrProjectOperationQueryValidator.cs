namespace Engineering.Application.Services.ProjectOperations.Queries.GetFltrProjectOperation;

public class GetFltrProjectOperationValidator : AbstractValidator<GetFltrProjectOperationQuery>
{
    public GetFltrProjectOperationValidator()
    {
        RuleFor(oo => oo.CostCenterId).IsPositive(ProjectOperationErrors.IdIsEmpty);
    }
}

