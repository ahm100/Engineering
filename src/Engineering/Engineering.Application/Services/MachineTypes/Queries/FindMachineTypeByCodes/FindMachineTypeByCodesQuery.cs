namespace Engineering.Application.Services.MachineTypes.Queries.FindMachineTypeByCodes;

public record FindMachineTypeByCodesQuery(
    List<string> Codes,
    long? CompanyId
    ) : IQuery<bool>;
