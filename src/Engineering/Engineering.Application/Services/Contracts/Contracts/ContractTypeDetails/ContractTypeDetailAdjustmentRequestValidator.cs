using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ContractTypeDetails;

public class ContractTypeDetailAdjustmentRequestValidator
    : AbstractValidator<ContractTypeDetailAdjustmentRequest>
{
    public ContractTypeDetailAdjustmentRequestValidator()
    {
        RuleFor(oo => oo.Type)
            .IsEnum(ContractCmts.ContractTypeDetailAdjustmentType);

        RuleFor(oo => oo)
            .Must(HasMatchingConfiguration)
            .WithError(ContractErrors.ContractTypeDetailAdjustmentTermsInvalid);

        RuleFor(oo => oo.PriceIndex)
            .SetValidator(new PriceIndexContractTypeDetailAdjustmentRequestValidator());

        RuleFor(oo => oo.Currency)
            .SetValidator(new CurrencyContractTypeDetailAdjustmentRequestValidator());

        RuleFor(oo => oo.Other)
            .SetValidator(new OtherContractTypeDetailAdjustmentRequestValidator());
    }

    private static bool HasMatchingConfiguration(
        ContractTypeDetailAdjustmentRequest request)
    {
        return request.Type switch
        {
            ContractTypeDetailAdjustmentType.PriceIndex =>
                request.PriceIndex is not null &&
                request.Currency is null &&
                request.Other is null,

            ContractTypeDetailAdjustmentType.Currency =>
                request.PriceIndex is null &&
                request.Currency is not null &&
                request.Other is null,

            ContractTypeDetailAdjustmentType.Other =>
                request.PriceIndex is null &&
                request.Currency is null &&
                request.Other is not null,

            _ => false
        };
    }
}
