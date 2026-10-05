namespace Engineering.Application.WebServices.MetaDataServices.ContractorEmployees.Models.GetById;

public record ContractorEmployeesByIdRequest(long ContractorId,
                                             int PageIndex,
                                             int PageSize);
