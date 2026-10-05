namespace Engineering.Application.Services.ProjectOperations.Queries.GetPODate;

public class GetPODateQueryValidator : AbstractValidator<GetPODateQuery>
{
    public GetPODateQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(ProjectErrors.IdIsEmpty);
    }
}