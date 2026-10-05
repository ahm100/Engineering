using Engineering.Application.WebServices.MetaDataServices.ContractorEmployees.Models;

namespace Engineering.Application.WebServices.MetaDataServices.ContractorEmployees.Queries.GetById;

public record ContractorEmployeesByIdQuery(long? ContractorId,
                                           int PageIndex,
                                           int PageSize) : IQuery<DataResult<List<ContractorEmployee>>>;
