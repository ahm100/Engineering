namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.DataModels;

public record GetExpertsByProjectOperationIdsModel
(
    long Id,
    decimal Number,
    string? Name,
    string? Code
);
