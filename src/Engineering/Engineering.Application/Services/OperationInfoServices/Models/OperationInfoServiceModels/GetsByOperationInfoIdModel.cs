namespace Engineering.Application.Services.OperationInfoServices.Models.OperationInfoServiceModels;

public record GetsByOperationInfoIdModel(
    long Id,
    string ServiceInfoName,
    string ServiceInfoCode,
    string TimeSpant,
    string? MeasurementName
    );
