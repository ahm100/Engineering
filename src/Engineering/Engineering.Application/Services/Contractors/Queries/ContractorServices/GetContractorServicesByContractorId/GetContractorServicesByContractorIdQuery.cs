using Engineering.Domain.Entities.ContractorServices;

namespace Engineering.Application.Services.Contractors.Queries.ContractorServices.GetContractorServicesByContractorId;

public record GetContractorServicesByContractorIdQuery(
    long ContractorId,
    string? FilterData,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IQuery<DataResult<List<ContractorService>>>;

