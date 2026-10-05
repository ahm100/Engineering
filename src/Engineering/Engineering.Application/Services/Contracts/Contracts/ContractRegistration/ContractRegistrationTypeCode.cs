using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ContractRegistration;

public static class ContractRegistrationTypeCode
{
    private static readonly (char Code, ContractTypeKind Kind)[] Mapping =
    [
        ('E', ContractTypeKind.Engineering),
        ('P', ContractTypeKind.Procurement),
        ('C', ContractTypeKind.Construction),
        ('S', ContractTypeKind.Services)
    ];

    public static bool TryParse(string? value)
        => TryParse(value, out _);

    public static bool TryParse(string? value, out IReadOnlyList<ContractTypeKind> kinds)
    {
        var normalized = value?.Trim().ToUpperInvariant();
        if (string.IsNullOrEmpty(normalized) || normalized.Distinct().Count() != normalized.Length)
        {
            kinds = [];
            return false;
        }

        var selected = Mapping.Where(x => normalized.Contains(x.Code)).ToList();
        if (selected.Count != normalized.Length)
        {
            kinds = [];
            return false;
        }

        kinds = selected.Select(x => x.Kind).ToList();
        return true;
    }

    public static string Format(IEnumerable<ContractTypeKind> kinds)
    {
        var set = kinds.ToHashSet();
        return new string(Mapping.Where(x => set.Contains(x.Kind)).Select(x => x.Code).ToArray());
    }
}
