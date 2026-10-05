namespace Engineering.Application.Services.ContractorMachineries.Commands.UpdateContractorMachinery;

public class UpdateContractorMachineryCommandValidator : AbstractValidator<UpdateContractorMachineryCommand>
{
    public UpdateContractorMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ContractorMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.Machinery).NotEmpty().WithError(ContractorMachineryErrors.InValidMachinery);
        RuleFor(oo => oo.ContractorId).NotEmpty().WithError(ContractorMachineryErrors.ContractorIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.CurrencyId).NotEmpty().WithError(ContractorMachineryErrors.CurrencyIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.Unit).NotEmpty().WithError(ContractorMachineryErrors.InValidUnit)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum);
        RuleFor(oo => oo.IsActive).NotNull().WithError(ContractorMachineryErrors.InValidIsActive);
        RuleFor(oo => oo.MachineryPrice).NotNull().WithError(ContractorMachineryErrors.PriceIsEmpty);
    }
}