namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts;

public class GetCStatementFContractsValidator : AbstractValidator<GetCStatementFContractsRequest>
{
    public GetCStatementFContractsValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
