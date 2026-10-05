using ContractorModel = Engineering.Application.WebServices.MetaDataServices.Contractors.Models.Contractor;

namespace Engineering.Application.WebServices.MetaDataServices.Contractors.Queries.GetContractorById;

public record GetContractorByIdQuery(
    long Id) : IQuery<ContractorModel?>;
