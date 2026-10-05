
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsContractorServiceByProjectOperationId;

public class GetsContractorServiceByProjectOperationIdQueryValidator : AbstractValidator<GetsContractorServiceByProjectOperationIdQuery>
{
    public GetsContractorServiceByProjectOperationIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().WithError(ContractorServiceErrors.ProjectOperationDetailIdIsEmpty);
    }
}