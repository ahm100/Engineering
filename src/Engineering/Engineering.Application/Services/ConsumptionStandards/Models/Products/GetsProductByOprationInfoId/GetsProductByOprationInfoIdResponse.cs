using Engineering.Application.Services.ConsumptionStandards.Models.Products.ProducModels;

namespace Engineering.Application.Services.ConsumptionStandards.Models.Products.GetsProductByOprationInfoId;

public record GetsProductByOprationInfoIdResponse(
    List<GetsProductByOperationInfoIdModels?> Data,
    int RowCount);
