
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetContractorServiceById;

public class GetContractorServiceByIdValidator : AbstractValidator<GetContractorServiceByIdRequest>
{
    public GetContractorServiceByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ContractorServiceErrors.IdIsEmpty);
    }
}
