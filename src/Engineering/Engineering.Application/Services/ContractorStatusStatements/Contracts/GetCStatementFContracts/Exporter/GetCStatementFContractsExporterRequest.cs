using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts.Enum;

namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts.Exporter;

public record GetCStatementFContractsExporterRequest(
    long Id,
    List<GetCStatementFContractsEnum>? CSFContractFilters,
    List<GetCStatementFContractsDailiesEnum>? CSFDailyFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
