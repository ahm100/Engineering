
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.UpdateContractorServiceServiceInfo;

public class UpdateContractorServiceServiceInfoCommandValidator : AbstractValidator<UpdateContractorServiceServiceInfoCommand>
{
    public UpdateContractorServiceServiceInfoCommandValidator()
    {
        RuleFor(oo => oo.ContractorServices).NotNull().WithError(ContractorServiceErrors.IdIsEmpty);
        RuleFor(oo => oo.OperationInfoServices).NotEmpty().WithError(ContractorServiceErrors.ServiceIdIsEmpty);
    }
}