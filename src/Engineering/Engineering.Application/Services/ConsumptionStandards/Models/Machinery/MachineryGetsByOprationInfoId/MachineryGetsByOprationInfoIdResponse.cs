using Engineering.Application.Services.ConsumptionStandards.Models.Machinery.MachineryModels;

namespace Engineering.Application.Services.ConsumptionStandards.Models.Machinery.MachineryGetsByOprationInfoId;

public record MachineryGetsByOprationInfoIdResponse(
    List<MachineryGetsByOperationInfoIdModel?> Data,
    int RowCount);
