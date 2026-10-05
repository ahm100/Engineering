
namespace Engineering.Application.Services.RequestMachineryStatusStatements.Commands.CreateRequestMachineryStatusStatementDetail;

public class CreateRequestMachineryStatusStatementDetailCommandValidator : AbstractValidator<CreateRequestMachineryStatusStatementDetailCommand>
{
    public CreateRequestMachineryStatusStatementDetailCommandValidator()
    {
        RuleFor(c => c.RequestMachinery)
             .NotNull().WithError(RequestMachineryStatusStatementErrors.InValidRequestMachinery);

        RuleFor(c => c.RequestMachineryStatusStatement)
              .NotNull().WithError(RequestMachineryStatusStatementErrors.InValidstatusStatement);

        RuleFor(c => c.Project)
              .NotNull().WithError(RequestMachineryStatusStatementErrors.InValidProject);

        RuleFor(c => c.Machinery)
            .NotNull().WithError(RequestMachineryStatusStatementErrors.InValidMachinery);

        RuleFor(c => c.ContractorId)
            .NotNull().WithError(RequestMachineryStatusStatementErrors.InValidContractorId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(c => c.RequestedCount)
            .NotNull().WithError(RequestMachineryStatusStatementErrors.InValidTotalRequestedCount);

        RuleFor(c => c.FinalPrice)
            .NotNull().WithError(RequestMachineryStatusStatementErrors.InValidTotalFinalPrice);

        RuleFor(c => c.Unit)
            .NotNull().WithError(RequestMachineryStatusStatementErrors.InValidUnit)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum);
    }
}
