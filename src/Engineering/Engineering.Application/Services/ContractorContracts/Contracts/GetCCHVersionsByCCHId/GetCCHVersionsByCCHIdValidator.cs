
namespace Engineering.Application.Services.BillOfLadings.Contracts.GetCCHVersionsById;

public class GetCCHVersionsByIdValidator : AbstractValidator<GetCCHVersionByCCHIdRequest>
{
    public GetCCHVersionsByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(CCCmts.ContractorContractHeaderVersionId);
    }
}