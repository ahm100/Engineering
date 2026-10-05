namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractStructure;

public class UpdateContractStructureValidator
    : AbstractValidator<UpdateContractStructureRequest>
{
    public UpdateContractStructureValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.Id);

        RuleFor(oo => oo.Items)
            .NotEmpty()
            .WithError(
                GlobalErrors.RequiredEmpty(
                    GlobalCmts.ContractType));

        RuleForEach(oo => oo.Items).ChildRules(item =>
        {
            item.RuleFor(oo => oo.Id)
                .IsOptionalPositive(GlobalCmts.Id);

            item.RuleFor(oo => oo.Kind)
                .IsEnum(GlobalCmts.ContractTypeKind);

            item.RuleFor(oo => oo.PricingMethod)
                .IsEnum(GlobalCmts.PricingMethod);
        });

        RuleFor(oo => oo.Items)
            .Must(items =>
            {
                if (items is null)
                    return true;

                var ids = items
                    .Where(oo => oo.Id.HasValue)
                    .Select(oo => oo.Id!.Value)
                    .ToList();

                return ids.Count == ids.Distinct().Count();
            })
            .WithError(
                GlobalErrors.NoDuplicates(
                    GlobalCmts.Id));

        RuleFor(oo => oo.Items)
            .Must(items =>
            {
                if (items is null)
                    return true;

                var kinds = items
                    .Select(oo => oo.Kind)
                    .ToList();

                return kinds.Count == kinds.Distinct().Count();
            })
            .WithError(
                GlobalErrors.NoDuplicates(
                    GlobalCmts.ContractTypeKind));
    }
}
