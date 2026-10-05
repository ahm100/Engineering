using Engineering.Application.Services.ConsumptionStandards.Models.Products.ProducModels;

namespace Engineering.Application.Services.ConsumptionStandards.Models.Products.GetsNonStandardProductByOprationInfoId;

public record GetsNonStandardProductByOprationInfoIdResponse(
    List<GetsNonStandardProductByOprationInfoIdModels?> Data,
    int RowCount);
