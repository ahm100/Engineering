namespace Engineering.Application.Services.MachineTypes.Queries.FindDuplicateMachineType;

public record FindDuplicateMachineTypeQuery(
    List<string> Names,
    List<string> Codes,
    List<int> FromWeights,
    List<int> UntilWeights,
    List<int> CabinTypCodes,
    long? CompanyId
    ) : IQuery<bool>;
