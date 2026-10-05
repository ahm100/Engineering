namespace Engineering.Application.Services.ConsumptionStandards.Models.Experts.ExpertModels;

public record ExpertGetsByOperationInfoIdModel(
    long OperationInfoExpertId,
    long Id,
    string? Name,
    string? Code,
    decimal ExpertNumber,
    string TimeSpant,
    decimal? UnusedPercentage
    );
