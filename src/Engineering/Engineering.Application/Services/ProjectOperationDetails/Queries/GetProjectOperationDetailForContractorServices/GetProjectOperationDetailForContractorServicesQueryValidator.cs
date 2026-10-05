namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailForContractorServices;

public class GetProjectOperationDetailForContractorServicesQueryValidator : AbstractValidator<GetProjectOperationDetailForContractorServicesQuery>
{
    public GetProjectOperationDetailForContractorServicesQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
