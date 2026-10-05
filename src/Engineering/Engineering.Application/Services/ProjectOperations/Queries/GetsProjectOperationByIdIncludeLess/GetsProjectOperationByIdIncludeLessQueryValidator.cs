namespace Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationByIdIncludeLess;

public class GetsProjectOperationByIdIncludeLessQueryValidator : AbstractValidator<GetsProjectOperationByIdIncludeLessQuery>
{
    public GetsProjectOperationByIdIncludeLessQueryValidator()
    {
        RuleForEach(oo => oo.Ids).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
