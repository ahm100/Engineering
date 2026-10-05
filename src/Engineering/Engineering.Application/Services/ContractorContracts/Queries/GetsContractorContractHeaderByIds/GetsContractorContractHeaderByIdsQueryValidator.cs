
namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractHeaderByIds;

public class GetsContractorContractHeaderByIdsQueryValidator : AbstractValidator<GetsContractorContractHeaderByIdsQuery>
{
    public GetsContractorContractHeaderByIdsQueryValidator()
    {
        RuleFor(oo => oo.Ids)
           .NotNull().WithError(CSSErrors.ContractorStatusStatementWithIdNotFound);
    }
}
