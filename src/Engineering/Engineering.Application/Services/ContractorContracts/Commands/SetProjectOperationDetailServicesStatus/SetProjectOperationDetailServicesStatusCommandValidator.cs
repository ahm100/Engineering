namespace Engineering.Application.Services.ContractorContracts.Commands.SetProjectOperationDetailServicesStatus;

public class SetProjectOperationDetailServicesStatusCommandValidator : AbstractValidator<SetProjectOperationDetailServicesStatusCommand>
{
    public SetProjectOperationDetailServicesStatusCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(ProjectOperationDetailErrors.ServiceInfoIdsIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(c => c.Status)
            .NotNull().WithError(GlobalErrors.StatusIsNull)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);

    }
}
