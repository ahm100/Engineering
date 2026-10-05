
namespace Engineering.Application.Services.ContractorContracts.Queries.GetFilteredProjectOperations;

public class GetFilteredProjectOperationsQueryValidator : AbstractValidator<GetFilteredProjectOperationsQuery>
{
    public GetFilteredProjectOperationsQueryValidator()
    {
        RuleFor(c => c.ProjectOperationIds)
            .NotEmpty().WithError(ContractorContractErrors.ProjectOperationIdsIsEmpty);

        RuleForEach(c => c.ProjectOperationIds)
            .GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
