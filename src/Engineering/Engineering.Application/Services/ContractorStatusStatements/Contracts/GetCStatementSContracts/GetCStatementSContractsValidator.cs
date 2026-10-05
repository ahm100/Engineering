namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts;

public class GetCStatementSContractsValidator : AbstractValidator<GetCStatementSContractsRequest>
{
    public GetCStatementSContractsValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
