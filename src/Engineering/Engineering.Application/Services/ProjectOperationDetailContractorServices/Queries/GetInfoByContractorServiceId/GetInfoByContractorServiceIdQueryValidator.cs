
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetProjectOperationDetailByIds;
public class GetInfoByContractorServiceIdQueryValidator : AbstractValidator<GetInfoByContractorServiceIdQuery>
{
    public GetInfoByContractorServiceIdQueryValidator()
    {
        RuleFor(oo => oo.ContractorServiceIds).NotNull().WithError(ContractorServiceErrors.ContractorServiceRequestsIsEmpty);

    }
}
