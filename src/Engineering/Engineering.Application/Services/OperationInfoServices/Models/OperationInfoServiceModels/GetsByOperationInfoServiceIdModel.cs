namespace Engineering.Application.Services.OperationInfoServices.Models.OperationInfoServiceModels;

public record GetsByOperationInfoServiceIdModel(long Id,
                                                string ServiceInfoName,
                                                string ServiceInfoCode,
                                                string? MeasurementName);