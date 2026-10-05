namespace Engineering.Application.Services.ProjectOperationWbses.Queries.GetProjectOperationWbsById;

public class GetProjectOperationWbsByIdQueryValidator : AbstractValidator<GetProjectOperationWbsByIdQuery>
{
    public GetProjectOperationWbsByIdQueryValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}