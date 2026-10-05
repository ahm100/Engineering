
namespace Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;

public class CreateContractorContractValidator : AbstractValidator<CreateContractorContractRequest>
{
    public CreateContractorContractValidator()
    {
        RuleFor(oo => oo.ContractorId)
            .IsPositive(GlobalCmts.ContractorId);

        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);


        RuleFor(oo => oo.CurrencyId)
            .IsPositive(CCCmts.CurrencyId);


        When(oo => oo.FixContractors != null, () =>
        {
            RuleForEach(oo => oo.FixContractors)
                .NotEmpty().SetValidator(new CreateFixCCValidator());
        });

        When(oo => oo.ProfessionalWorkdayContractors != null, () =>
        {
            RuleForEach(oo => oo.ProfessionalWorkdayContractors)
                .NotEmpty().SetValidator(new CreateProfessionalWorkdayCCValidator());
        });

        When(oo => oo.ServiceContractors != null, () =>
        {
            RuleForEach(oo => oo.ServiceContractors)
                .NotEmpty().SetValidator(new CreateServiceCCValidator());
        });

        When(x => x.ItemPriceListContracts != null, () =>
        {
            RuleForEach(x => x.ItemPriceListContracts)
                .NotNull();

            RuleForEach(x => x.ItemPriceListContracts)
                .SetValidator(new CreateItemPriceListCCValidator());
        });
    }
}
