
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetContractorServiceById;

public record GetContractorServiceByIdResponse(
    long Id,
    long ProjectOperationDetailId,
    long ServiceInfoId,
    string ServiceInfoName,
    string ServiceInfoCode,
    long? ContractorId,
    string? FullName,
    string? OrganizationCode,
    decimal Volume,
    string TimeSpant,
    bool IsActive,
    PODContractorServiceType Type
    );

