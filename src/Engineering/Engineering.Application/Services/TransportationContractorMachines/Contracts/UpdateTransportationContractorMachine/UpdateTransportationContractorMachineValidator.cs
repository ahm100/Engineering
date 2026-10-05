namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.UpdateTransportationContractorMachine;

public class UpdateTransportationContractorMachineValidator : AbstractValidator<UpdateTransportationContractorMachineRequest>
{
    public UpdateTransportationContractorMachineValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(TransportationContractorMachineCmts.TransportationContractorMachineId);

        RuleFor(c => c.NumberPlate)
            .IsFullString(TransportationContractorMachineCmts.NumberPlate, 10, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.MachineTypeId)
            .IsPositive(TransportationContractorMachineCmts.MachineTypeId);

        RuleFor(c => c.ContractorId)
            .IsPositive(TransportationContractorMachineCmts.TransportationContractorId);

        When(c => c.ContractorPersonnels != null && c.ContractorPersonnels.Count > 0, () =>
        {
            RuleForEach(c => c.ContractorPersonnels)
                .IsPositive(TransportationContractorMachineCmts.contractorPersonnelId);
        });
    }
}