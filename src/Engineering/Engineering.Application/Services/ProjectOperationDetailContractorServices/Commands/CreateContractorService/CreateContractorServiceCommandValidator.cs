
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.CreateContractorService;

public class CreateContractorServiceCommandValidator : AbstractValidator<CreateContractorServiceCommand>
{
    public CreateContractorServiceCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetail).NotEmpty().WithError(ContractorServiceErrors.ProjectOperationDetailIdIsEmpty);
        RuleFor(oo => oo.OperationInfoService).NotEmpty().WithError(ContractorServiceErrors.ServiceIdIsEmpty);
        RuleFor(oo => oo.Volume).GreaterThan(0).WithError(ContractorServiceErrors.VolumeMustGreaterThan);
    }
}