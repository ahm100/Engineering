namespace Engineering.Application.Services.ContractorMachineries.Models.UpdateContractorMachinery;

public class UpdateContractorMachineryValidator : AbstractValidator<UpdateContractorMachineryRequest>
{
    public UpdateContractorMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotEmpty().WithError(ContractorMachineryErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.MachineryId).NotEmpty().WithError(ContractorMachineryErrors.InValidContractorMachinery)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
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
