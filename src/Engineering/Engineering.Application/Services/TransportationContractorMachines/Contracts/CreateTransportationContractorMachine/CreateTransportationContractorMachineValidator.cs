namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.CreateTransportationContractorMachine;

public class CreateTransportationContractorMachineValidator : AbstractValidator<CreateTransportationContractorMachineRequest>
{
    public CreateTransportationContractorMachineValidator()
    {
        RuleFor(c => c.TransportationContractorId)
            .IsPositive(TransportationContractorMachineCmts.TransportationContractorId);

        RuleForEach(c => c.ContractorMachines)
            .SetValidator(new CreateTransportationContractorMachineModelValidator());
    }
}

public class CreateTransportationContractorMachineModelValidator : AbstractValidator<CreateTransportationContractorMachineModel>
{
    public CreateTransportationContractorMachineModelValidator()
    {
        RuleFor(c => c.NumberPlate)
            .IsFullString(TransportationContractorMachineCmts.NumberPlate, 10, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.MachineTypeId)
            .IsPositive(TransportationContractorMachineCmts.MachineTypeId);

        When(c => c.ContractorPersonnels != null && c.ContractorPersonnels.Count > 0, () =>
        {
            RuleForEach(c => c.ContractorPersonnels)
                .IsPositive(TransportationContractorMachineCmts.contractorPersonnelId);
        });
    }
}