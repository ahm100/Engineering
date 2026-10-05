namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetProjectOperationDetailContractorServices;

public class GetProjectOperationDetailContractorServicesQueryValidator : AbstractValidator<GetProjectOperationDetailContractorServicesQuery>
{
    public GetProjectOperationDetailContractorServicesQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(DailyProjectOperationErrors.InValidProjectOperationDetailId);
    }
}
