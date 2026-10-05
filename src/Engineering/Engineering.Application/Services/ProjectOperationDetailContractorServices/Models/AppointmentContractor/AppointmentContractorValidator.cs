
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.AppointmentContractor;

public class AppointmentContractorValidator : AbstractValidator<AppointmentContractorRequest>
{
    public AppointmentContractorValidator()
    {
        RuleFor(oo => oo.ContractorServiceIds).NotEmpty().WithError(ContractorServiceErrors.IdIsEmpty);
        RuleFor(oo => oo.ContractorId).NotNull().WithError(ContractorServiceErrors.ContactorIdIsEmpty);
    }
}
