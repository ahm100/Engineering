
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.UpdateContractorService;

public class UpdateContractorServiceCommandValidator : AbstractValidator<UpdateContractorServiceCommand>
{
    public UpdateContractorServiceCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ContractorServiceErrors.IdIsEmpty);
        RuleFor(oo => oo.OperationInfoService).NotEmpty().WithError(ContractorServiceErrors.ServiceIdIsEmpty);
        RuleFor(oo => oo.Volume).GreaterThan(0).WithError(ContractorServiceErrors.VolumeMustGreaterThan);
    }
}