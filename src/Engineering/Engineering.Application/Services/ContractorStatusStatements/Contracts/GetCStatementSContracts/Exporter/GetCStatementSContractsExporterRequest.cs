using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts.Enum;

namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts.Exporter;

public record GetCStatementSContractsExporterRequest(
    long Id,
    List<GetCStatementSContractsEnum>? CSSContractFilters,
    List<GetCStatementSContractsDailiesEnum>? CSSDailyFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
