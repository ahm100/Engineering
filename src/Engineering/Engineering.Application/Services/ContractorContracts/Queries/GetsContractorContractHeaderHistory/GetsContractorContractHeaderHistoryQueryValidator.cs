
namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractHeaderHistory;

public class GetsContractorContractHeaderHistoryQueryValidator : AbstractValidator<GetsContractorContractHeaderHistoryQuery>
{
    public GetsContractorContractHeaderHistoryQueryValidator()
    {
        RuleFor(c => c.ContractorContractId)
            .NotNull().WithError(ContractorContractErrors.ContractorContractIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.PageIndex)
            .GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);

        RuleFor(oo => oo.PageSize)
            .GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);

        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex)
            .GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}
