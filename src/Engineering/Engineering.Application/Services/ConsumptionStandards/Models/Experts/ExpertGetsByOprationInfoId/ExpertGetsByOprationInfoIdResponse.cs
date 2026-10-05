using Engineering.Application.Services.ConsumptionStandards.Models.Experts.ExpertModels;

namespace Engineering.Application.Services.ConsumptionStandards.Models.Experts.ExpertGetsByOprationInfoId;

public record ExpertGetsByOprationInfoIdResponse(
    List<ExpertGetsByOperationInfoIdModel?> Data,
    int RowCount);
