namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Responses;

public record ContractorServiceDataModel(
    long Id,
    long? ProjectServiceDetailId,
    long? ContractorId,
    string? FullName,
    string? OrganizationCode,
    long ServiceInfoId,
    string? ServiceInfoName,
    string? ServiceInfoCode,
    long? UnitOfMeasurementId,
    string? MeasurementName,
    decimal Volume,
    decimal UsedVolume,
    decimal RemaindVolume,
    string TimeSpant,
    long? ProjectServiceId,
    long? ProjectServiceInfoId,
    string? ProjectServiceName,
    string? ProjectServiceCode,
    long? ProjectServiceUnitOfMeasurementId,
    string? ProjectServiceMeasurementName,
    decimal? ProjectServiceVolume,
    decimal? ProjectServiceDoneVolume,
    decimal? ProjectServiceRemaindVolume,
    bool HasContractorExpert,
    bool IsActive
 );
