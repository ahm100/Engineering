namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderGroupStatusChanger;

public class ContractorContractHeaderGroupStatusChangerValidator : AbstractValidator<ContractorContractHeaderGroupStatusChangerRequest>
{
    public ContractorContractHeaderGroupStatusChangerValidator()
    {
        RuleFor(x => x.Ids)
            .NotNull()
            .WithError(GlobalErrors.RequiredNull("Ids"))
            .NotEmpty()
            .WithError(GlobalErrors.RequiredEmpty("Ids"));

        RuleForEach(x => x.Ids!)
            .IsPositive("Id");

        RuleFor(x => x.Ids)
            .HasNoDuplicates(
                x => x,
                "Ids");

        RuleFor(x => x.Status)
            .IsEnum("Status");
    }
}
