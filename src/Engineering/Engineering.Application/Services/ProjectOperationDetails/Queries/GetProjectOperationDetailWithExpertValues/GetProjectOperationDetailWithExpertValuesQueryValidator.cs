
namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithExpertValues;

public class GetProjectOperationDetailWithConsumableVolumeExpertsQueryValidator : AbstractValidator<GetProjectOperationDetailWithExpertValuesQuery>
{
    public GetProjectOperationDetailWithConsumableVolumeExpertsQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
