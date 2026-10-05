
namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByIds;

public class GetsProjectOperationDetailByIdsQueryValidator : AbstractValidator<GetsProjectOperationDetailByIdsQuery>
{
    public GetsProjectOperationDetailByIdsQueryValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(ProjectOperationDetailErrors.UnValidIds);
    }
}