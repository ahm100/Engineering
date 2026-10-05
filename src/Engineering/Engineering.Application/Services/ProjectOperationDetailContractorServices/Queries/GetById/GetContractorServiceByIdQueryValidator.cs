
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetById;

public class GetContractorServiceByIdQueryValidator : AbstractValidator<GetContractorServiceByIdQuery>
{
    public GetContractorServiceByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ContractorServiceErrors.IdIsEmpty);
    }
}