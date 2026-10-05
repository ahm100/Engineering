namespace Engineering.Application.Services.EmployerContracts.Contracts.GetCommonData;

public record GetCommonDataRequest(
    List<long> CurrencyIds,
    List<long> EmployerIds,
    List<long> CreatorIds
);
