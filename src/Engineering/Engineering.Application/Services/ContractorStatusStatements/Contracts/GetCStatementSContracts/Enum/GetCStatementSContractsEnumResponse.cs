namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts.Enum;

public record GetCStatementSContractsEnumResponse(
    List<EnumObject> ServiceData,
    List<EnumObject> DailyData
    );
