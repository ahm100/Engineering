namespace Engineering.Application.Services.Contractors.Models.ContractorServices.CreateContractorsService;

public record CreateContractorServicesRequestModel(long? Id,
                                                   long ServiceInfoId,
                                                   bool IsDeleted,
                                                   bool IsActive);