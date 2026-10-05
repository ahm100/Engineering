namespace Engineering.Application.Services.ContractorMachineries.Commands.CreateContractorMachinery;

public class CreateContractorMachineryCommandValidator : AbstractValidator<CreateContractorMachineryCommand>
{
    public CreateContractorMachineryCommandValidator()
    {
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