
using Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract.ItemPriceList;

namespace Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract;

public class UpdateContractorContractValidator : AbstractValidator<UpdateContractorContractRequest>
{
    public UpdateContractorContractValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.ContractorContractId);

        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);

        RuleFor(oo => oo.CurrencyId)
            .IsPositive(CCCmts.CurrencyId);

        When(oo => oo.UpdateFixContractors != null, () =>
        {
            RuleForEach(oo => oo.UpdateFixContractors)
                .NotEmpty()
                .SetValidator(new UpdateFixCCValidator());
        });

        When(oo => oo.UpdateProfessionalWorkdayContracts != null, () =>
        {
            RuleForEach(oo => oo.UpdateProfessionalWorkdayContracts)
                .NotEmpty()
                .SetValidator(new UpdateProfessionalWorkdayCCValidator());
        });

        When(oo => oo.UpdateServiceContractors != null, () =>
        {
            RuleForEach(oo => oo.UpdateServiceContractors)
                .NotEmpty()
                .SetValidator(new UpdateServiceCCValidator());
        });

        When(x => x.UpdateItemPriceListContracts != null, () =>
        {
            RuleForEach(x => x.UpdateItemPriceListContracts)
                .NotNull()
                .SetValidator(
                    new UpdateItemPriceListCCValidator());
        });

        When(x => x.CreateItemPriceListContracts != null, () =>
        {
            RuleForEach(x => x.CreateItemPriceListContracts)
                .NotNull()
                .SetValidator(
                    new CreateItemPriceListCCValidator());
        });
    }
}
