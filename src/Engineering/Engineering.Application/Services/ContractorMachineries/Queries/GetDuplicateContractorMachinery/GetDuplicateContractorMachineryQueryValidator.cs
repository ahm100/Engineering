namespace Engineering.Application.Services.ContractorMachineries.Queries.GetDuplicateContractorMachinery;

public class GetDuplicateContractorMachineryQueryValidator : AbstractValidator<GetDuplicateContractorMachineryQuery>
{
    public GetDuplicateContractorMachineryQueryValidator()
    {
        RuleFor(oo => oo.MachineryId).NotEmpty().WithError(ContractorMachineryErrors.InValidContractorMachinery)
           .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.ContractorId).NotEmpty().WithError(ContractorMachineryErrors.ContractorIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.Unit).NotEmpty().WithError(ContractorMachineryErrors.InValidUnit)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum);
    }
}