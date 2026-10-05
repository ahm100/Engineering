using Engineering.Application.Services.Contracts.Contracts.ContractChanges;

namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractChange;

public class UpdateContractChangeValidator : AbstractValidator<UpdateContractChangeRequest>
{
    public UpdateContractChangeValidator()
    {
        RuleFor(oo => oo.ContractId).IsPositive(GlobalCmts.ContractId);
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
        RuleFor(oo => oo.Type).IsEnum(ContractCmts.ContractChangeType);
        RuleFor(oo => oo.Number).HasMaxLength(ContractCmts.ContractChangeNumber, 250);
        RuleFor(oo => oo.Date).IsDate(GlobalCmts.Date);
        RuleFor(oo => oo.Subject).HasMaxLength(ContractCmts.ContractChangeSubject, 250);
        RuleFor(oo => oo.DurationChange)
            .Must(value => !value.HasValue || value.Value != 0)
            .WithError(ContractErrors.ContractChangeDurationInvalid);
        RuleFor(oo => oo.Urls).NotNull().NotEmpty();
        RuleForEach(oo => oo.Urls).HasMaxLength(GlobalCmts.Url, 1500);
        RuleForEach(oo => oo.Items)
            .SetValidator(new ContractChangeItemRequestValidator());
    }
}
