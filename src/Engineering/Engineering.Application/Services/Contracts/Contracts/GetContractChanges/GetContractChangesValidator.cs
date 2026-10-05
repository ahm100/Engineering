namespace Engineering.Application.Services.Contracts.Contracts.GetContractChanges;

public class GetContractChangesValidator : AbstractValidator<GetContractChangesRequest>
{
    private static readonly HashSet<string> SortableFields =
        new(StringComparer.OrdinalIgnoreCase)
        {
            nameof(GetContractChangesModel.Id),
            nameof(GetContractChangesModel.ContractId),
            nameof(GetContractChangesModel.Type),
            nameof(GetContractChangesModel.Number),
            nameof(GetContractChangesModel.Date),
            nameof(GetContractChangesModel.Subject),
            nameof(GetContractChangesModel.FinancialChangeAmount),
            nameof(GetContractChangesModel.DurationChange),
            nameof(GetContractChangesModel.NewContractAmount),
            nameof(GetContractChangesModel.DocumentCount)
        };

    public GetContractChangesValidator()
    {
        RuleFor(oo => oo.ContractId).IsPositive(GlobalCmts.ContractId);
        RuleFor(oo => oo.Type).IsNullableEnum(ContractCmts.ContractChangeType);
        RuleFor(oo => oo.DateFrom)
            .Must(value => !value.HasValue || value.Value != default);
        RuleFor(oo => oo.DateTo)
            .Must(value => !value.HasValue || value.Value != default);
        RuleFor(oo => oo.PageIndex).PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(oo => oo.PageSize).PageSizeZero(GlobalCmts.PageSize);
        RuleFor(oo => oo.OrderBy)
            .Must(HasOnlySortableFields)
            .WithMessage("Invalid OrderBy field.");
    }

    private static bool HasOnlySortableFields(string[]? orderBy)
    {
        if (orderBy is null || orderBy.Length == 0)
            return true;

        foreach (var item in orderBy)
        {
            if (string.IsNullOrWhiteSpace(item))
                return false;

            var parts = item.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length is < 1 or > 2 || !SortableFields.Contains(parts[0]))
                return false;

            if (parts.Length == 2 &&
                !parts[1].Equals("asc", StringComparison.OrdinalIgnoreCase) &&
                !parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
        }

        return true;
    }
}
