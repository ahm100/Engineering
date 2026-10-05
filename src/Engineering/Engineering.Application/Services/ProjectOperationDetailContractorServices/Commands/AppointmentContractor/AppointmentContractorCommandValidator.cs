
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.AppointmentContractor;

public class AppointmentContractorCommandValidator : AbstractValidator<AppointmentContractorCommand>
{
    public AppointmentContractorCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ContractorServiceErrors.IdIsEmpty);
        RuleFor(oo => oo.ContractorId).NotNull().WithError(ContractorServiceErrors.ContactorIdIsEmpty);
    }
}
