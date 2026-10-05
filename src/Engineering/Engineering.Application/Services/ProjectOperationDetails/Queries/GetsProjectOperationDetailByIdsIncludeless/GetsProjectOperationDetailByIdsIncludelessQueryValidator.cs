namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByIdsIncludeless;

public class GetsProjectOperationDetailByIdsIncludelessQueryValidator : AbstractValidator<GetsProjectOperationDetailByIdsIncludelessQuery>
{
    public GetsProjectOperationDetailByIdsIncludelessQueryValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(ProjectOperationDetailErrors.UnValidIds);
    }
}